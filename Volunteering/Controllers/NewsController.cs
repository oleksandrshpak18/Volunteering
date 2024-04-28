using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Volunteering.ApplicationServices;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;
using Volunteering.Helpers;

namespace Volunteering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        public NewsApplicationService _service;
        public NewsController(NewsApplicationService service)
        {
            _service = service;
        }

        [HttpGet("get-all"), AllowAnonymous]
        [ProducesResponseType(typeof(List<NewsVM>), 200)] 
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost("add"), Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(NewsVM), 200)]
        [Consumes("multipart/form-data")]
        public IActionResult Add([FromForm]NewsVM vm)
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst("UserId")?.Value);

            if (ModelState.IsValid)
            {
                return Ok(_service.Add(userId, vm));
            }
            return BadRequest();
        }

        [HttpPut("update"), Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(NewsVM), 200)]
        [Consumes("multipart/form-data")]
        public IActionResult Update([FromForm] NewsVM vm)
        {
            if (ModelState.IsValid)
            {
                var res = _service.Update(vm);
                if(res != null)
                {
                    return Ok(res);
                }
                else
                {
                    return NotFound(res);
                }
            }
            return BadRequest();
        }
    }
}
