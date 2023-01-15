namespace Utility.ResponseMapper
{
    public interface IResponse<T> where T : new()
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}