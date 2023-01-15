namespace Data.Common
{
    public partial class BaseEntityCommon : BaseEntityDate
    {
        public int DisplayOrder { get; set; } = 0;
        public bool Active { get; set; } = false;
    }
}
