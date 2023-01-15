using System;
using System.ComponentModel.DataAnnotations;

namespace Data.SMS
{
    public partial class Company_SenderID
    {
        [Key]
        public Int64 Id { get; set; }
        public int SenderID_ID { get; set; }
        public int SenderID_CompanyID { get; set; }
        public string SenderID_Text { get; set; }
        public int SenderID_TotalSMS { get; set; }
        public int SenderID_UsedSMS { get; set; }
        public bool SenderID_Enabled { get; set; }
    }
}
