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

        [Required(ErrorMessage = "Please enter your Invoice Reference")]
        [Display(Name = "Invoice Reference")]
        public string InvoiceReference { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter the amount you'd like to pay")]
        [Display(Name = "Amount (£)")]
        public decimal Amount { get; set; }

        public string PaymentStatus { get; set; }
        public string PaymentSuccessId { get; set; }
        public string ExceptionMesssge { get; set; }

        [Required]
        [Display(Name = "Credit Card No")]
        public string CreditCardNo { get; set; }

        [Required]
        [Display(Name = "Last 3 digits of signature strip")]
        public int CVV2 { get; set; }

        [Required]
        [Display(Name="Expiry Month")]
        public int expireMonth { get; set; }

        [Required]
        [Display(Name = "Expiry Year")]
        public int expireYear { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name="Last Name")]
        public string lastName { get; set; }

        [Required]
        [Display(Name = "Card Type")]
        public string creditType { get; set; }

        public PaypalModel()
        {
            cardTypes = new List<SelectListItem> {  new SelectListItem { Value = "mastercard", Text = "Mastercard" },
                                                    new SelectListItem { Value = "visa", Text = "Visa" },
                                                    new SelectListItem {Value = "Visa Electron", Text = "Visa Electron"},
                                                    new SelectListItem {Value = "Maestro", Text = "Maestro"}
                                                };

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