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

        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResult), 200)]
        [Consumes("multipart/form-data")]
        public IActionResult Register([FromForm] UserRegisterRequest vm) 
        {
            if(ModelState.IsValid)
            {
                var res = _service.Register(vm);
                if (res.Result == true)
                {
                    return Ok(res);
                }
                else
                {
                    return BadRequest(res);
                }
            }
             return BadRequest();
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResult), 200)]
        public IActionResult Login([FromBody] UserLoginRequestVM vm)
        {
            if (ModelState.IsValid)
            {
                var res = _service.Login(vm);
                if (res.Result == true)
                {
                    return Ok(res);
                }
                else
                {
                    return BadRequest(res);
                }
            }

            return BadRequest();
        }

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(List<UserVM>), 200)]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }
    }
}
