using System;

namespace Utility.Models.Frontend.CustomizedModel
{
    public class ASSubscriptionModel
    {
        public int Id { get; set; }
        public int Confirmed { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int DeliveryTimeSlotId { get; set; }
        public decimal MonthlyPaymentAmount { get; set; }
    }
}
