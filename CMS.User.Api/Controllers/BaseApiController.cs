using CMS.User.Api.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace CMS.User.Api.Controllers
{
    public class BaseApiController: ControllerBase
    {
        protected ActionResult NotFoundResponse(string errorCode, string errorMessage)
        {
            var response = new ErrorResponse(errorCode, errorMessage);
            return NotFound(response);
        }

        protected ActionResult BadRequestResponse(string errorCode, string errorMessage)
        {
            var response = new ErrorResponse(errorCode, errorMessage);
            return BadRequest(response);
        }
    }
}
