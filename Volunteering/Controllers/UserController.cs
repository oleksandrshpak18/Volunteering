using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Security.Claims;
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
        public IActionResult Register([FromBody] UserRegisterRequest vm) 
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

        [HttpPost("login2"), AllowAnonymous]
        [ProducesResponseType(typeof(AuthResult), 200)]
        public IActionResult Login2([FromBody] UserLoginRequestVM vm)
        {
            if (ModelState.IsValid)
            {
                var res = _service.Login(vm);

                

                if (res.Result == true)
                {
                    var token = res.Token;
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true, // Should be set to true in production
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddHours(1) // Adjust as necessary
                    };

                    // Set the token as a cookie
                    Response.Cookies.Append("accessToken", token, cookieOptions);

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

        [HttpGet("get"), AllowAnonymous]
        [ProducesResponseType(typeof(UserPublicInfoVM), 200)]
        public IActionResult GetById([FromQuery]Guid userId)
        {
            return Ok(_service.GetPublicById(userId));
        }

        [HttpGet("get-user-details"), Authorize(Roles ="Registered, Admin")]
        [ProducesResponseType(typeof(UserVM), 200)]
        public IActionResult GetById([FromQuery]Guid ?id=null)
        {
            var role = HttpContext.User.FindFirst(ClaimTypes.Role).Value;
            if(id == null)
            {
                Guid userId = Guid.Parse(HttpContext.User.FindFirst("UserId")?.Value);
                return Ok(_service.GetById(userId));
            } 
            else if (role == "Admin")
            {
                return Ok(_service.GetById(id.Value));
            } 
            else
            {
                return BadRequest();
            }
            
        }

        [HttpGet("get-top"), AllowAnonymous]
        [ProducesResponseType(typeof(List<UserShortInfoVM>), 200)]
        public IActionResult GetTop([FromQuery] int count = 7)
        {
            return Ok(_service.GetTop(count));
        }

        [HttpGet("get-short-info"), Authorize(Roles = "Registered, Admin")]
        [ProducesResponseType(typeof(List<UserShortInfoVM>), 200)]
        public IActionResult GetShortInfo()
        {
            Guid userId = Guid.Parse(HttpContext.User.FindFirst("UserId")?.Value);
            return Ok(_service.GetShortInfo(userId));
        }

        [HttpGet("is-info-filled"), Authorize(Roles = "Registered, Admin")]
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
