using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace gpPaymentServicesMvc.Models
{
    public class PaypalModel
    {
        public class RegisterExternalLoginModel
        {
            [Required]
            [Display(Name = "Invoice Reference")]
            public string InvoiceReference { get; set; }

            [Required]
            [Display(Name = "Amount")]
            public decimal Amount { get; set; }
        }
    }
}