namespace WebApplication1.DTOs.Response
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new();

        public ErrorResponse(int statusCode, string message, List<string> errors)
        {
            StatusCode = statusCode;
            Message = message;
            Errors = errors;
        }
    }
}
