using System.Net;

namespace CodePulse.API.Models.DTO.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Status = HttpStatusCode.OK;
            Message = "success";
        }
        public HttpStatusCode Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public string? Errors { get; set; }
    }
}
