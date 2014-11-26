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

    }
}