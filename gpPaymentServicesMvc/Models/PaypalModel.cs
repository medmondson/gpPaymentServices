using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace gpPaymentServicesMvc.Models
{
    public class PaypalModel
    {
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

    }
}