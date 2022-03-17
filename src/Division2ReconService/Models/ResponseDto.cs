namespace Division2ReconService.Models
{
    public class ResponseDto<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class ResponseErrorDto<T> : ResponseDto<T>
    {
        public string ErrorCode { get; set; }
    }
}
