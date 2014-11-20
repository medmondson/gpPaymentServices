using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gpPaymentServicesMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Paypal()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpPost]
        public ActionResult PaypalSubmit(string invoiceReference, decimal amount)
        {
            return Redirect("/");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
