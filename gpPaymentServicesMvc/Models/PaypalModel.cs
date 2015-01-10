using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.WebPages.Html;

namespace gpPaymentServicesMvc.Models
{
    public class PaypalModel
    {

        public IEnumerable<SelectListItem> cardTypes { get; set; }
        public IEnumerable<SelectListItem> monthsOfYear { get; set; }

        [Required]
        [Display(Name = "Invoice Reference")]
        public string InvoiceReference { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        public string PaymentStatus { get; set; }
        public string PaymentSuccessId { get; set; }

        public string CreditCardNo { get; set; }
        public int CVV2 { get; set; }
        public int expireMonth { get; set; }
        public int expireYear { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string creditType { get; set; }

        public PaypalModel()
        {
            cardTypes = new List<SelectListItem> {  new SelectListItem { Value = "Mastercard", Text = "Mastercard" },
                                                    new SelectListItem { Value = "Visa", Text = "Visa" } };

            monthsOfYear = new List<SelectListItem> {
                new SelectListItem{Value="1", Text="1"},
                new SelectListItem{Value="2", Text="2"},
                new SelectListItem{Value="3", Text="3"},
                new SelectListItem{Value="4", Text="4"},
                new SelectListItem{Value="5", Text="5"},
                new SelectListItem{Value="6", Text="6"},
                new SelectListItem{Value="7", Text="7"},
                new SelectListItem{Value="8", Text="8"},
                new SelectListItem{Value="9", Text="9"},
                new SelectListItem{Value="10", Text="10"},
                new SelectListItem{Value="11", Text="11"},
                new SelectListItem{Value="12", Text="12"}
            
            };

        }

    }
}