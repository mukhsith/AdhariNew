using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plugin.Payment.KNET.Models
{
    public class PaymentResponseDetailsModel
    {
        public string Amount { get; set; }
        public string PaymentId { get; set; }
        public string TrackId { get; set; }
        public string TransId { get; set; }
        //public string Language { get; set; }
        public string RefId { get; set; }
        public string EntityId { get; set; }
        public string CustomerId { get; set; }
        public string RequestType { get; set; }
        public string Auth { get; set; }
        public string Result { get; set; }
        public string ErrorText { get; set; }
        public bool IsExceptionError { get; set; }
        public string ExceptionErrorText { get; set; }
    }
}