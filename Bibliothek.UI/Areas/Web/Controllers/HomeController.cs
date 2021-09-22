using Bibliothek.DAL.Context;
using Bibliothek.Model.Entity;
using Bibliothek.Service.Option;
using Bibliothek.UI.Areas.Web.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Bibliothek.UI.Areas.Web.Models;
using System.Net.Mail;
using System.Text;
using Bibliothek.Utility;
using Newtonsoft.Json;
using System.Net;

namespace Bibliothek.UI.Areas.Web.Controllers
{
    public class HomeController : Controller
    {
        CategoryService _categoryService;
        CustomerService _customerService;
        ProductService _productService;
        OrderService _orderService;
        OrderDetailService _orderDetailService;
        SliderService _sliderService;
        MenuService _menuService;
        MailingService _mailingService;
        AddressService _addressService;
        CityService _cityService;
        DistrictService _districtService;


        public HomeController()
        {
            _categoryService = new CategoryService();
            _customerService = new CustomerService();
            _productService = new ProductService();
            _orderService = new OrderService();
            _orderDetailService = new OrderDetailService();
            _sliderService = new SliderService();
            _menuService = new MenuService();
            _mailingService = new MailingService();
            _addressService = new AddressService();
            _cityService = new CityService();
            _districtService = new DistrictService();
        }

        public ActionResult Index()
        {
            List<Product> productList = _productService.GetActive().ToList();
            ViewBag.RandomProducts = (from p in productList orderby Guid.NewGuid() select p).Take(9).ToList();

            List<Slider> model = _sliderService.GetActive();
            return View(model);


        }

        //public ActionResult SearchResult()
        //{
        //    List<Product> productList = _productService.GetActive().ToList();
        //    ViewBag.Search = (from s in productList orderby Guid.NewGuid() select s).Take(6).ToList();
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult SearchResult(ProductVM data)
        //{

        //    using (ProjectContext db = new ProjectContext())
        //    {
        //        var results = db.Products.FirstOrDefault(x => x.Name == data.Name);


        //            return View(data);

        //    }
        //}

        public ActionResult SearchResult(ProductVM data)
        {
            List<Product> productList = _productService.GetActive().ToList();
            ViewBag.Search = (from s in productList orderby Guid.NewGuid() select s).Take(6).ToList();

            using (ProjectContext db = new ProjectContext())
            {
                var result = db.Products.FirstOrDefault(x => x.Name == data.Name);
            }

            return View(data);
        }


        public ActionResult Category()
        {
            List<Category> model = _categoryService.GetActive();
            return PartialView("_Category", model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM data)
        {
            if (ModelState.IsValid)
            {
                using (ProjectContext db = new ProjectContext())
                {
                    var user = db.Customers.FirstOrDefault(x => x.Email == data.Email && x.Password == data.Password);

                    if (user != null)
                    {
                        Session["login"] = user;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Mail adresi veya şifre hatalı!");
                    }
                }
            }

            return View(data);
        }

        public ActionResult Logout()
        {
            Session.Remove("login");
            return RedirectToAction("Index", "Home", new { Area = "Web" });
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterVM data, Customer model)
        {
            if (ModelState.IsValid)
            {
                using (ProjectContext db = new ProjectContext())
                {
                    var customerEmail = db.Customers.FirstOrDefault(x => x.Email == data.Email);

                    if (customerEmail == null)
                    {
                        _customerService.Add(model);
                        Session["login"] = model;
                        return RedirectToAction("RegisterOk");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Mail adresi sistemde kayıtlı.");
                    }
                }
            }
            return View();
        }


        public ActionResult RegisterOk()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

       

        [HttpPost]
        public ActionResult SignUp(NonRegisterVM data)
        {
            if (ModelState.IsValid)
            {
                using (ProjectContext db = new ProjectContext())
                {
                    var customerEmail = db.Customers.FirstOrDefault(x => x.Email == data.Email);

                    if (customerEmail == null)
                    {
                        Customer customer = new Customer();
                        customer.Name = data.Name;
                        customer.LastName = data.LastName;
                        customer.Email = data.Email;
                        customer.Phone = data.Phone;
                        customer.Password = "";
                        _customerService.Add(customer);
                        Session["login"] = customer;
                        return RedirectToAction("Index", "Cart");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Mail adresi sistemde kayıtlı.");
                        return RedirectToAction("Login");
                    }
                }
            }
            return View();
        }


        public ActionResult MyOrders()
        {
            var customer = Session["login"] as Customer;
            if (customer == null)
                return RedirectToAction("Login");

            List<Order> model = _orderService.ListOrderHistory(customer.ID);


            ViewBag.oCount = model.Count;
            
            return View(model);
        }

        public ActionResult MyAddress()
        {
            var customer = Session["login"] as Customer;
            if (customer == null)
                return RedirectToAction("Login");

            List<Address> model = _addressService.GetByCustomerId(customer.ID);

            return View(model);
        }

        [HttpPost]
        public JsonResult MyAddress(Guid? cityID, string type)
        {
            List<City> cityList = _cityService.GetActive().ToList();
            List<District> districtList = _districtService.GetActive().ToList();

            List<SelectListItem> result = new List<SelectListItem>();
            bool isSuccess = true;

            try
            {
                switch (type)
                {
                    case "getCity":

                        foreach (var city in cityList.OrderBy(x => x.CityName).ToList())
                        {
                            result.Add(new SelectListItem
                            {
                                Text = city.CityName,
                                Value = city.ID.ToString()
                            });
                        }
                        break;

                    case "getDistrict":

                        foreach (var district in districtList.Where(city => city.CityID == cityID).OrderBy(x => x.DistrictName).ToList())
                        {
                            result.Add(new SelectListItem
                            {
                                Text = district.DistrictName,
                                Value = district.ID.ToString()
                            });
                        }
                        break;

                    default:
                        break;

                }
            }
            catch (Exception)
            {
                isSuccess = false;
                result = new List<SelectListItem>();
                result.Add(new SelectListItem
                {
                    Text = "Bir hata oluştu :(",
                    Value = "Default"
                });

            }

            return Json(new { ok = isSuccess, text = result });
        }

        [HttpPost]
        public ActionResult SaveAddress(Address address)
        {
            var user = Session["login"] as Customer;
            if (user == null)
                return RedirectToAction("Login");

            address.CustomerID = user.ID;
            _addressService.Add(address);

            var msg = "İşlem yapılırken bir hata oluştu";
            bool ok = false;
            if (address.ID != Guid.Empty)
            {
                ok = true;
                msg = "işlem Başarılı";
            }
            return Json(new { msg, ok }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DeleteAddress(Guid id)
        {
            _addressService.Remove(id);
            return Redirect("/Web/Home/MyAddress");
        }

        public ActionResult MyOrderDetails(Guid id)
        {
            List<OrderDetail> model = _orderDetailService.GetDefault(x => x.Order.ID == id);
            return View(model);
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactForm model)
        {
            if (ModelState.IsValid)
            {
                var body = new StringBuilder();
                body.AppendLine("İsim: " + model.FullName);
                body.AppendLine("Eposta: " + model.Email);
                body.AppendLine("Tel: " + model.Phone);
                body.AppendLine("Konu: " + model.Message);

                var response = Request["g-recaptcha-response"];
                const string secret = "6LfQkmkUAAAAAJ0bOtb8Xf8AXVfQunq15czGlUFf";

                var client = new WebClient();
                var reply =
                    client.DownloadString(
                        string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

                var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

                if (!captchaResponse.Success)
                {
                    TempData["Message"] = "Lütfen güvenliği doğrulayınız.";
                    return Redirect("/Web/Home/Contact");
                }

                else
                {
                    if (model.Email != null)
                    {
                        Gmail.SendMail(body.ToString());
                        ViewBag.Success = true;
                        return Redirect("/Web/Home/Index");
                    }
                }
            }

            return View();
        }

        public class CaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("error-codes")]
            public List<string> ErrorCodes { get; set; }
        }


        public ActionResult Footer()
        {
            List<Menu> model = _menuService.GetActive();
            return PartialView("_PartialFooter", model);
        }

        public ActionResult SendMailing()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SendMailing(Mailing model)
        {
            if (!string.IsNullOrEmpty(model.Email))
            {
                using (ProjectContext db = new ProjectContext())
                {
                    var Email = db.Mailings.FirstOrDefault(x => x.Email == model.Email);

                    if (Email == null)
                    {
                        _mailingService.Add(model);
                        //Gmail.SendMail(data.Email);
                        return Json(new { done = true, message = "Mail adresiniz kaydedilmiştir." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { done = false, message = "Mail adresi sistemde kayıtlı." }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new { done = false, message = "Lütfen Email Adresi Giriniz!" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Content(Guid id)
        {
            Menu model = _menuService.GetByID(id);
            return View(model);
        }

        public ActionResult MyFavorites()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddToMyFavorites(Guid id)
        {
            Product product = _productService.GetByID(id);
            FavoriteProductVM model = new FavoriteProductVM
            {
                ID = product.ID,
                Image = product.ImagePath,
                Name = product.Name,
                Price = product.Price
            };

            if (Session["favorite"] != null)
            {
                FavoriteProductCart favorite = Session["favorite"] as FavoriteProductCart;
                favorite.AddFavorite(model);
                Session["favorite"] = favorite;
            }
            else
            {
                FavoriteProductCart favorite = new FavoriteProductCart();
                favorite.AddFavorite(model);
                Session.Add("favorite", favorite);
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public JsonResult FavoriteList()
        {
            if (Session["favorite"] != null)
            {

                FavoriteProductCart favorite = Session["favorite"] as FavoriteProductCart;
                List<FavoriteProductVM> productList = favorite.FavoriteProductList.Select(x => new FavoriteProductVM
                {
                    ID = x.ID,
                    Image = x.Image,
                    Name = x.Name,
                    Price = x.Price
                }).ToList();
                return Json(productList, JsonRequestBehavior.AllowGet);
            }
            return Json("Empty", JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveFavorite(Guid id)
        {

            FavoriteProductCart favoriteCart = Session["favorite"] as FavoriteProductCart;
            favoriteCart.RemoveFavorite(id);
            Session["favorite"] = favoriteCart;
            return Json("");
        }
    }
}