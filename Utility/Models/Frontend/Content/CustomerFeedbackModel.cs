using System;
using Utility.ResponseMapper;

namespace Utility.Models.Frontend.Content
{
    public class CustomerFeedbackModel
    { 
            public string Name { get; set; }
            public string MobileNumber { get; set; }
            public string EmailAddress { get; set; }
            public string Subject { get; set; }
            public string Message { get; set; }
            public int Status { get; set; }

        public static implicit operator CustomerFeedbackModel(APIResponseModel<CustomerFeedbackModel> v)
        {
            throw new NotImplementedException();
        }
    }
}
