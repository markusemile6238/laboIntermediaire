namespace Agency.API.Dtos
{
    public class ApiResponse
    {
        public string Status { get; set; }
        public int ? StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Detail { get; set; }
        public Object? Data { get; set; }


        public ApiResponse()
        {
        }

        public ApiResponse(string status, int? statusCode) // success
        {
            Status = status;
            StatusCode = statusCode;
        }

        public ApiResponse(string status, int? statusCode, string? message, string? detail) : this(status, statusCode) //error
        {
            Message = message;
            Detail = detail;

        }

        //  avec retour de données
        public ApiResponse(string status, int? statusCode, string? message, string? detail, object? data) : this(status, statusCode, message, detail)
        {
            Data = data;
        }

    }
}
