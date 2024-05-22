using CodePulse.API.Models.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CodePulse.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
       protected OkObjectResult ReturnResponse(object value = null, string message = null, string? errorCode = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            BaseResponse response = new BaseResponse();
            if (!string.IsNullOrEmpty(message))
                response.Message = message;
            if (errorCode != null)
                response.Errors = errorCode;
            if (value != null)
            {
                response.Data = value;
                var status = value.GetType().GetProperty("StatusCode");
                if (status != null)
                    response.Status = statusCode;
            }

            return Ok(response);
        }
    }
}
