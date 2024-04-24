using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volunteering.ApplicationServices;
using Volunteering.Data.ViewModels;
using Volunteering.Helpers;

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

        // TODO: remaster usage of UserVM to use UserRegisterRequestVM here instead of UserVM
        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResult), 200)]
        [Consumes("multipart/form-data")]
        public IActionResult Register([FromForm] UserVM vm) 
        {
            // TODO: change the logic of return types here
            if(ModelState.IsValid)
            {
                return Ok(_service.Register(vm));
            }
            return BadRequest();
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResult), 200)]
        public IActionResult Login([FromBody] UserLoginRequestVM vm)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.Login(vm));
            }

            return BadRequest( new AuthResult()
            {
                Result = false,
                Messages = new List<string>() { "Invalid payload"}  
            });
        }

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(List<UserVM>), 200)]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }
    }
}
