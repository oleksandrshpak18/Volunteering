using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("register"), AllowAnonymous]
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

        [HttpPost("login"), AllowAnonymous]
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

        [HttpGet("get-all"), Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<UserVM>), 200)]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("get-top"), AllowAnonymous]
        [ProducesResponseType(typeof(List<UserShortInfoVM>), 200)]
        public IActionResult GetTop()
        {
            return Ok(_service.GetTop());
        }

        [HttpGet("is-info-filled"), Authorize(Roles = "Registered")]
        [ProducesResponseType(typeof(Boolean), 200)]
        public IActionResult IsInfoFiled()
        {
            Guid userId = Guid.Parse(HttpContext.User.FindFirst("UserId")?.Value);
            bool ?res = _service.IsInfoFilled(userId);
            if(res == null)
            {
                return BadRequest("User not found");
            }

            return Ok(res);    
        }

        [HttpPatch("update"), Authorize(Roles = "Registered")]
        [ProducesResponseType(typeof(UserVM), 200)]
        public IActionResult Update([FromForm]UserDetailsVM user)
        {
            var userId = Guid.Parse(HttpContext.User.FindFirst("UserId")?.Value);
            UserVM? res = _service.Update(userId, user);
            if (res == null)
            {
                return BadRequest("User not found");
            }

            return Ok(res);
        }
    }
}
