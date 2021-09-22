using Bibliothek.Model.Entity;
using Bibliothek.Service.Option;
using Bibliothek.UI.Areas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bibliothek.UI.Areas.Web.Controllers
{
    public class CheckoutController : Controller
    {
        OrderService _orderService;
        ProductService _productService;
        CustomerService _customerService;
        OrderDetailService _orderDetailService;

        public CheckoutController()
        {
            _orderService = new OrderService();
            _productService = new ProductService();
            _customerService = new CustomerService();
            _orderDetailService = new OrderDetailService();
        }

        Order o = new Order();

        [HttpPost]
        public ActionResult Add(string myAddress)
        {            
            var a1 = Request.Form["myAddress"];

            Customer customer;

            customer = Session["login"] as Customer;

            o.CustomerID = customer.ID;

            Session["address"] = Guid.Parse(a1);
            
            return RedirectToAction("Add");
        }



        public ActionResult Add()
        {

            if (Session["sepet"] == null)
            {
                return Redirect("~/Home/Index");
            }

            ProductCart cart = Session["sepet"] as ProductCart;

            Customer customer = null;
            if (Session["login"] != null)
            {
                customer = Session["login"] as Customer;
            }

            o.CustomerID = customer.ID;
            o.Phone = customer.Phone;

            _customerService.DetachEntity(customer);

            Product p;

            Random random = new Random();
            int orderNo = random.Next(111111, 999999);

            foreach (var item in cart.CartProductList)
            {
                p = _productService.GetByID(item.ID);
                p.UnitsInStock -= item.Quantity;
                _productService.Update(p);

                o.OrderDetails.Add(new OrderDetail
                {
                    ProductID = p.ID,
                    Price = p.Price,
                    Quantity = item.Quantity
                });

                _productService.DetachEntity(p);

                o.OrderNo = orderNo;
            }

            o.AddressID = Guid.Parse(Session["address"].ToString());
            _orderService.Add(o);

            Session["order"] = o;
            Session["login"] = customer;

            return RedirectToAction("OrderCompleted");
        }

        public ActionResult OrderCompleted()
        {
            Session["sepet"] = null;
            return View();
        }

        //[HttpPost]
        //public JsonResult Add(Address AddressID)
        //{

        //    o.Address.ID = Guid.Parse(AddressID.ToString());
        //    return Json("Adres Seçildi", JsonRequestBehavior.AllowGet);
        //}
    }
}