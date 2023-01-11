namespace Utility.Models.Frontend.CustomerManagement
{
    public class AddressModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public int GovernorateId { get; set; }
        public string GovernorateName { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public string Block { get; set; }
        public string Street { get; set; }
        public string Avenue { get; set; }
        public string HouseNumber { get; set; }
        public string BuildingNumber { get; set; }
        public string FloorNumber { get; set; }
        public string FlatNumber { get; set; }
        public string SchoolName { get; set; }
        public string MosqueName { get; set; }
        public string GovernmentEntity { get; set; }
        public string Notes { get; set; }
        public string AddressText { get; set; }
        public bool Selected { get; set; }
    }
}
