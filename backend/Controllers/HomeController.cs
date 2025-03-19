using Microsoft.AspNetCore.Mvc;

namespace ProdOps.Api.Controllers
{

    [ApiController]  // Marks the class as an API controller and enables model binding and validation.
    [Route("")]  // Sets the route pattern for this controller.
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult GetHome()
        {
            return Ok("Welcome to the API!");
        }
    }
}
