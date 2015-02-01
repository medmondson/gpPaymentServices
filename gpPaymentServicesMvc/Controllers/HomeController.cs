using System;
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

            //Set defaults
            PaypalModel model = new PaypalModel
            {
                CreditCardNo = "4417119669820331",
                creditType = "visa",
                expireMonth = 11,
                expireYear = 2018,
                firstName = "Joe",
                lastName = "Shopper",
                CVV2 = 874
            };

            return View(model);
        }

        public ActionResult ThankYou()
        {
            PaypalModel model = new PaypalModel
            {
                PaymentSuccessId = "AG5H4B"
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PaypalModel model)
        {
            //TODO: Tidy all this up

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

            Payment createdPayment = null;

            try
            {

                createdPayment = pymnt.Create(accessToken);
            }
              
            catch(PayPal.Exception.PayPalException e)
            {
                //return view here with details
                model.ExceptionMesssge = e.Message;
                return View("ThankYou", model);
            }

            var state = createdPayment.state;
            var id = createdPayment.id;

            model.PaymentStatus = state;
            model.PaymentSuccessId = id;
  
            return View("ThankYou",model);
            
        }

    }
}
