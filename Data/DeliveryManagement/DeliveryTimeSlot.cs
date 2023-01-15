using Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Helpers;

namespace Data.DeliveryManagement
{

    public class DeliveryTimeSlot : BaseEntityCommon
    {
        public int DayId { get; set; }

        [StringLength(Constants.ShortDataSize)]
        public string NameEn { get; set; }

        [StringLength(Constants.ShortDataSize)]
        public string NameAr { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int MaximumOrders { get; set; }

        [NotMapped]
        public string StartTimeOnly { get; set; }
        [NotMapped]
        public string EndTimeOnly { get; set; }

        public string GetStartTime()
        {
            return DateTime.Today.Add(StartTime).ToString("hh:mm tt");
        }

        public string GetEndTime()
        {
            return DateTime.Today.Add(EndTime).ToString("hh:mm tt");
        }

        public void SetStartTime(string time)
        {
            this.StartTime = DateTime.Parse(time).TimeOfDay;
        }

        public void SetEndTime(string time)
        {
            this.EndTime = DateTime.Parse(time).TimeOfDay;
        }
    }
}
