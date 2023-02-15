namespace Utility.Models.Frontend.CustomizedModel
{
    public class KeyValuPairModel
    {
        public KeyValuPairModel()
        {
            Title = string.Empty;
            TitleColor = string.Empty;
            Value = string.Empty;
            ValueColor = string.Empty;
        }
        public string Title { get; set; }
        public string TitleColor { get; set; }
        public bool TitleBold { get; set; }
        public bool TitleBig { get; set; }
        public string Value { get; set; }
        public string ValueColor { get; set; }
        public bool ValueBold { get; set; }
        public bool ValueBig { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public int DisplayOrder { get; set; }
    }
}
