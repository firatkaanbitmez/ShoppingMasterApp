namespace ShoppingMasterApp.API.Responses
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public string ResponseStatus { get; set; }
        public string Message { get; set; }
    }
}
