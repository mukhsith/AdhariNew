using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plugin.Payment.KNET.Models
{
    public class PaymentUrlRequestModel
    {
        public string LangId { get; set; }
        public string Amount { get; set; }
        public string TrackId { get; set; }
        public string EntityId { get; set; }
        public string CustomerId { get; set; }
        public string RequestType { get; set; }
    }
}