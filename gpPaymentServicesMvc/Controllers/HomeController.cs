using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gpPaymentServicesMvc.Models;

namespace gpPaymentServicesMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var properties = PayPal.Manager.ConfigManager.Instance.GetProperties();

            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        [HttpGet]
        public ActionResult Paypal()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpPost]
        public ActionResult Paypal(string invoiceReference, decimal amount)
        {
            var properties = PayPal.Manager.ConfigManager.Instance.GetProperties();
            return Redirect("/");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
