using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("")]
        public IActionResult HandleError(Exception errorMessage)
        {
            
            // Return 500 with a generic error message
            return StatusCode(500, new { error = errorMessage });
        }
    }
}

