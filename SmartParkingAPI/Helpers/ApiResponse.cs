namespace SmartParking.API.Helpers
{
    public class ApiResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public ApiResponse(T? data, string message = "", bool status = true)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}
