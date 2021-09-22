using Bibliothek.Model.Entity;
using Bibliothek.Service.Option;
using Bibliothek.UI.Areas.Web.Models;
using Bibliothek.UI.Areas.Web.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bibliothek.UI.Areas.Web.Controllers
{
    public class CartController : Controller
    {
        ProductService _productService;
        CustomerService _customerService;
        AddressService _addressService;


        public CartController()
        {
            _productService = new ProductService();
            _customerService = new CustomerService();
            _addressService = new AddressService();
        }

        //public ActionResult Index()
        //{
        //    Customer customer = Session["login"] as Customer;
        //    List<Address> model = _addressService.GetByCustomerId(customer.ID);

        //    return View(model);
        //}

        public ActionResult Index()
        {
            Customer customer = Session["login"] as Customer;

            if (customer != null)
            {
                List<Address> model = _addressService.GetByCustomerId(customer.ID);
                return View(model);
            }
            else

                return View();

            
        }

        public JsonResult List()
        {
            if (Session["sepet"] != null)
            {
                
                ProductCart cart = Session["sepet"] as ProductCart;
                List<ProductVM> productList = cart.CartProductList.Select(x => new ProductVM
                {
                    ID = x.ID,
                    Name = x.Name,
                    Price = x.Price,
                    UnitsInStock = x.UnitsInStock,
                    Quantity = x.Quantity
                }).ToList();
                return Json(productList, JsonRequestBehavior.AllowGet);
            }
            return Json("Empty", JsonRequestBehavior.AllowGet);
        }

        //public JsonResult AddressList()
        //{
        //        Customer customer = Session["login"] as Customer;
        //        List<Address> addressList = customer.Addresses.Select(x => new Address
        //        {
        //            ID = x.ID,
        //            AddressTitle = x.AddressTitle,
        //            AddressContent = x.AddressContent
        //        }).ToList();
        //        return Json(addressList, JsonRequestBehavior.AllowGet);       
        //}

        [HttpPost]
        public JsonResult Add(Guid id)
        {
            Product product = _productService.GetByID(id);
            ProductVM model = new ProductVM
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                UnitsInStock = product.UnitsInStock,
                Quantity = 1
            };

            if (Session["sepet"] != null)
            {
                ProductCart cart = Session["sepet"] as ProductCart;
                cart.AddCart(model);
                Session["sepet"] = cart;
            }
            else
            {
                ProductCart cart = new ProductCart();
                cart.AddCart(model);
                Session.Add("sepet", cart);
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public JsonResult IncreaseCart(Guid id)
        {
            ProductCart cart = Session["sepet"] as ProductCart;
            cart.IncreaseCart(id);
            Session["sepet"] = cart;
            return Json("");
        }

        public JsonResult DecreaseCart(Guid id)
        {
            if (Session["sepet"] != null)
            {
                ProductCart cart = Session["sepet"] as ProductCart;
                cart.DecreaseCart(id);
                Session["sepet"] = cart;
            }
            return Json("");
        }

        public JsonResult RemoveCart(Guid id)
        {

            ProductCart cart = Session["sepet"] as ProductCart;
            cart.RemoveCart(id);
            Session["sepet"] = cart;
            return Json("");
        }
    }
}