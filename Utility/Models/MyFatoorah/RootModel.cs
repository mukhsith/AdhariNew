namespace Utility.Models.MyFatoorah
{
    public class RootModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object ValidationErrors { get; set; }
        public DataModel Data { get; set; }
    }
}
