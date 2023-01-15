using Data.Common;
using Data.DeliveryManagement;
using Data.Locations;
using System.ComponentModel.DataAnnotations;
using Utility.Helpers;

namespace Data.CustomerManagement
{
    public partial class Address : BaseEntityCommon
    {
        public int CustomerId { get; set; }
        public int TypeId { get; set; }

        [StringLength(Constants.MediumDataSize)]
        public string Name { get; set; }

        [StringLength(Constants.MobileDataSize)]
        public string MobileNumber { get; set; }

        [StringLength(Constants.EmailDataSize)]
        public string EmailAddress { get; set; }
        public int AreaId { get; set; }

        [StringLength(Constants.MediumDataSize)]
        public string Block { get; set; }

        [StringLength(Constants.MediumDataSize)]
        public string Street { get; set; }

        [StringLength(Constants.MediumDataSize)]
        public string Avenue { get; set; }

        [StringLength(Constants.MediumDataSize)]
        public string HouseNumber { get; set; }

        [StringLength(Constants.MediumDataSize)]
        public string BuildingNumber { get; set; }

        [StringLength(Constants.MediumDataSize)]
        public string FloorNumber { get; set; }

        [StringLength(Constants.MediumDataSize)]
        public string FlatNumber { get; set; }

        [StringLength(Constants.MediumDataSize)]
        public string SchoolName { get; set; }

        [StringLength(Constants.MediumDataSize)]
        public string MosqueName { get; set; }

        [StringLength(Constants.MediumDataSize)]
        public string GovernmentEntity { get; set; }
        public int? AddressId { get; set; }
        public string Notes { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Area Area { get; set; }
    }
}
