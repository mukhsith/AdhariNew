using Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Helpers;

namespace Data.Content
{
    public class SocialMediaLink:   BaseEntityDate
    {
        [StringLength(Constants.ExtraLargeDataSize)]
        public string InstagramLink { get; set; }
        [StringLength(Constants.ExtraLargeDataSize)]
        public string FacebookLink { get; set; }
        [StringLength(Constants.ExtraLargeDataSize)]
        public string TwitterLink { get; set; }
        [StringLength(Constants.ExtraLargeDataSize)]
        public string YoutubeLink { get; set; }

        [StringLength(Constants.ExtraLargeDataSize)]
        public string WhatsAppLink{ get; set; }
        [StringLength(Constants.ExtraLargeDataSize)]
        public string TiktokLink { get; set; }
        [StringLength(Constants.ExtraLargeDataSize)]
        public string SnapchatLink { get; set; } 
    }
}
