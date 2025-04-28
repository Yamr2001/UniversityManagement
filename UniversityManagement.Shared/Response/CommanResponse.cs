namespace UniversityManagement.Shared.Response
{
    public class CommonResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; } = [];

        public CommonResponse()
        {
            Success = true;
        }

        public CommonResponse(T data, string message = null)
        {
            Success = true;
            Data = data;
            Message = message;
        }

        public CommonResponse(string errorMessage)
        {
            Success = false;
            Message = errorMessage;
            Errors.Add(errorMessage);
        }

        public CommonResponse(IEnumerable<string> errors)
        {
            Success = false;
            Errors = errors.ToList();
            Message = "Multiple errors occurred";
        }
    }

    // Non-generic version for simple responses
    public class CommonResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public CommonResponse()
        {
            Success = true;
        }

        public static CommonResponse SuccessResponse(string message = null)
        {
            return new CommonResponse { Success = true, Message = message };
        }

        public static CommonResponse ErrorResponse(string errorMessage)
        {
            return new CommonResponse { Success = false, Message = errorMessage };
        }

        public static CommonResponse ErrorResponse(IEnumerable<string> errors)
        {
            return new CommonResponse { Success = false, Errors = errors.ToList() };
        }
    }
}