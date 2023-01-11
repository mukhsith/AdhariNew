using System.Collections.Generic;

namespace Utility.Models.Frontend.CustomizedModel
{
    public class HeaderMenuModel
    {
        public HeaderMenuModel()
        {

            Categories = new List<HeaderMenuModel>();
        }
        public int Id { get; set; }
        public bool Active { get; set; }
        public string Title { get; set; }
        public string Url  { get; set; }
        public IList<HeaderMenuModel> Categories { get; set; } 
    }
}
