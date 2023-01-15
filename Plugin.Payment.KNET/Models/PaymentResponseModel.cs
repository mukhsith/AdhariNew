using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plugin.Payment.KNET.Models
{
    public class PaymentResponseModel
    {
        public string Trandata { get; set; }
        public string ErrorText { get; set; }
    }
}