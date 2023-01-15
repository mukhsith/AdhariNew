using System;
using System.ComponentModel.DataAnnotations;

namespace Data.SMS
{
    public partial class SMS_Push
    {
        [Key]
        public Int64 Id { get; set; }
        public Int64 Push_ID { get; set; }
        public Int64  Push_MessageID { get; set; }
        public Int64 Push_UserID { get; set; }
        public string Push_ANI { get; set; }
        public string Push_DNIS { get; set; }
        public byte Push_OperatorID { get; set; }
        public string Push_Message { get; set; }
        public byte Push_Lang { get; set; }
        public DateTime Push_ScheduleDate { get; set; }
        public DateTime Push_Date { get; set; }
        public DateTime Push_SubmitDate { get; set; }
        public string Push_Result { get; set; }
        public string Push_TransactionID { get; set; }
        public string Push_NetPoints { get; set; }
        public string Push_RejectedNumbers { get; set; }
        public byte Push_Retry { get; set; }
        public byte Push_Status { get; set; }
    }
}
