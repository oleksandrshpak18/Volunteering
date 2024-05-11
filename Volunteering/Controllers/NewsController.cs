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

        [HttpGet("get-news-page"), AllowAnonymous]
        [ProducesResponseType(typeof(List<NewsVM>), 200)] 
        public IActionResult GePage([FromQuery] int page = 1)
        {
            return Ok(_service.GetPage(page));
        }

        [HttpGet("get"), AllowAnonymous]
        [ProducesResponseType(typeof(List<NewsVM>), 200)]
        public IActionResult GeById([FromQuery] Guid id)
        {
            if(ModelState.IsValid)
            {
                var res = _service.GetById(id);
                if(res == null)
                {
                    return NotFound();
                }
                return Ok(res);
            }

            return BadRequest();
        }

        [HttpGet("get-all"), Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<NewsVM>), 200)]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("get-recent"), AllowAnonymous]
        [ProducesResponseType(typeof(List<NewsVM>), 200)]
        public IActionResult GetRecent([FromQuery]int? count)
        {
            return Ok(_service.GetRecent(count));
        }

        [HttpPost("add"), Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(NewsVM), 200)]
        [Consumes("multipart/form-data")]
        public IActionResult Add([FromForm] NewsVM vm)
        {
            Guid userId = Guid.Parse(HttpContext.User.FindFirst("UserId")?.Value);

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
                Guid userId = Guid.Parse(HttpContext.User.FindFirst("UserId")?.Value);

                var res = _service.Update(userId, vm);
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


        [HttpDelete("delete"), Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(NewsVM), 200)]
        public IActionResult Delete([FromQuery] Guid newsId)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.Delete(newsId));
            }
            return BadRequest();
        }
    }
}
