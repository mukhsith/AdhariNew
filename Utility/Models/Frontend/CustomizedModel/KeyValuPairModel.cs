namespace Utility.Models.Frontend.CustomizedModel
{
    public class KeyValuPairModel
    {
        public string Title { get; set; }
        public string Value { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public int DisplayOrder { get; set; }
    }
}
