using System;
using System.Collections.Generic;
using Utility.Models.Admin.Sales;
using Utility.Models.Frontend.CustomerManagement; 

namespace Utility.Models.Admin
{
    public class AdminCustomerModel
    {
        
        public int Id { get; set; } 
        public string Name { get; set; }
        public string MobileNumber { get; set; } 
        public string EmailAddress { get; set; }
        public bool B2B { get; set; } 
        public int LanguageId { get; set; }
        //public IList<string> Addresses { get; set; }  
        public bool Guest { get; set; }   
       // public int CountryId { get; set; } 
       public int TotalOrders { get; set; }
       public DateTime CreatedOn { get; set; }
       public bool Active { get; set; }
       public bool Deleted { get; set; }
        public WalletModel Wallet  { get; set; }
        public List<AddressModel> Addresses { get; set; }
         
    }
}
