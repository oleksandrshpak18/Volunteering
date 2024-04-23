using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volunteering.ApplicationServices;
using Volunteering.Data.ViewModels;

namespace Volunteering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserApplicationService _service;
        public UserController(UserApplicationService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(UserVM), 200)]
        [Consumes("multipart/form-data")]
        public IActionResult Register([FromForm] UserVM vm)
        {
            return Ok(_service.Add(vm));
        }

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(List<UserVM>), 200)]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }
    }
}
