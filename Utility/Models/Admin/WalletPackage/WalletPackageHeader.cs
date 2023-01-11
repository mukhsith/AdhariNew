using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Models.Frontend.CouponPromotion;

namespace Utility.Models.Admin.WalletPackage
{
    public class WalletPackageHeader
    {
        public List<TopUpSale> Packages = new();
        public List<WalletPackageModel> DropdownList = new();
    }
}
