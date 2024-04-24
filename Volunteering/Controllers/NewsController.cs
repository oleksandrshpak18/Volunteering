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
    public class NewsController : ControllerBase
    {
        public NewsApplicationService _service;
        public NewsController(NewsApplicationService service)
        {
            _service = service;
        }

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(List<NewsVM>), 200)] 
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        //[HttpPost("add")]
        [HttpPost("add"), Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(NewsVM), 200)]
        [Consumes("multipart/form-data")]
        public IActionResult Add([FromForm]NewsVM vm)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.Add(vm));
            }
            return BadRequest();
        }
    }
}
