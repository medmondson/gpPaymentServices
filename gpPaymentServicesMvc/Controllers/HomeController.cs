﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gpPaymentServicesMvc.Models;
using PayPal;
using PayPal.Api.Payments;

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
        public ActionResult Paypal(PaypalModel model)
        {
            // Get a reference to the config
            var config = PayPal.Manager.ConfigManager.Instance.GetProperties();

            // Read the clientId and clientSecret stored in the config
            var clientId = config["clientId"];
            var clientSecret = config["clientSecret"];

            // Use OAuthTokenCredential to request an access token from PayPal
            var accessToken = new OAuthTokenCredential(clientId, clientSecret, config).GetAccessToken();

            var apiContext = new APIContext(accessToken);

            // Initialize the apiContext's configuration with the default configuration for this application.
            apiContext.Config = PayPal.Manager.ConfigManager.Instance.GetProperties();

            var version = apiContext.SdkVersion;

            //// ###Items
            //// Items within a transaction.
            //Item item = new Item();
            //item.name = "Energy Bill";
            //item.currency = "GBP";
            //item.price = "1";
            //item.quantity = "5";
            //item.sku = "sku";

            //List<Item> itms = new List<Item>();
            //itms.Add(item);
            //ItemList itemList = new ItemList();
            //itemList.items = itms;

            // ###Address
            // Base Address object used as shipping or billing
            // address in a payment.
            Address billingAddress = new Address();
            billingAddress.city = "Johnstown";
            billingAddress.country_code = "US";
            billingAddress.line1 = "52 N Main ST";
            billingAddress.postal_code = "43210";
            billingAddress.state = "OH";

            // ###CreditCard
            // A resource representing a credit card that can be
            // used to fund a payment.
            CreditCard crdtCard = new CreditCard();
            crdtCard.billing_address = billingAddress;
            crdtCard.cvv2 = 874;
            crdtCard.expire_month = 11;
            crdtCard.expire_year = 2018;
            crdtCard.first_name = "Joe";
            crdtCard.last_name = "Shopper";
            crdtCard.number = "4417119669820331";
            crdtCard.type = "visa";

            // ###Details
            // Let's you specify details of a payment amount.
            //Details details = new Details();
            //details.shipping = "1";
            //details.subtotal = "5";
            //details.tax = "1";

            // ###Amount
            // Let's you specify a payment amount.
            Amount amnt = new Amount();
            amnt.currency = "GBP";
            // Total must be equal to sum of shipping, tax and subtotal.
            amnt.total = model.Amount.ToString();
            //amnt.details = details;

            // ###Transaction
            // A transaction defines the contract of a
            // payment - what is the payment for and who
            // is fulfilling it. 
            Transaction tran = new Transaction();
            tran.amount = amnt;
            tran.description = "This is the payment transaction description.";
            //tran.item_list = itemList;
            tran.invoice_number = model.InvoiceReference;

            // The Payment creation API requires a list of
            // Transaction; add the created `Transaction`
            // to a List
            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(tran);

            // ###FundingInstrument
            // A resource representing a Payer's funding instrument.
            // For direct credit card payments, set the CreditCard
            // field on this object.
            FundingInstrument fundInstrument = new FundingInstrument();
            fundInstrument.credit_card = crdtCard;

            // The Payment creation API requires a list of
            // FundingInstrument; add the created `FundingInstrument`
            // to a List
            List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>();
            fundingInstrumentList.Add(fundInstrument);

            // ###Payer
            // A resource representing a Payer that funds a payment
            // Use the List of `FundingInstrument` and the Payment Method
            // as `credit_card`
            Payer payr = new Payer();
            payr.funding_instruments = fundingInstrumentList;
            payr.payment_method = "credit_card";

            // ###Payment
            // A Payment Resource; create one using
            // the above types and intent as `sale` or `authorize`
            Payment pymnt = new Payment();
            pymnt.intent = "sale";
            pymnt.payer = payr;
            pymnt.transactions = transactions;

            try
            {

                Payment createdPayment = pymnt.Create(accessToken);
            }
            catch(Exception e)
            {

            }

            var state = createdPayment.state;
            var id = createdPayment.id;

            model.PaymentStatus = state;

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
