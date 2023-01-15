namespace Utility.Models.Frontend.CustomizedModel
{
    public class ProductQueryModel
    {
        /// <summary>
        /// default value is false
        /// </summary>
        public bool IsEnglish { get; set; }

        /// <summary>
        /// default value is 1
        /// </summary>
        public int CountryId { get; set; } = 1;
        /// <summary>
        /// default value is null
        /// </summary>
        public string Keyword { get; set; } = null;
        /// <summary>
        /// default value is null
        /// </summary>public string Keyword   { get; set; } = null;
        public int? Id { get; set; } = null;

        /// <summary>
        /// default value is null
        /// </summary>
        public string SeoName { get; set; } = null;

        /// <summary>
        /// default value is null
        /// </summary>
        public int? CategoryId { get; set; } = null;

        /// <summary>
        /// default value is null
        /// </summary>
        public bool? Favorite { get; set; } = null;

        /// <summary>
        /// default value is null
        /// </summary>
        public int? CustomerId { get; set; } = null;

        /// <summary>
        /// default value is  null
        /// </summary>
        public string CustomerGuidValue { get; set; } = null;
    }
}
