using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volunteering.ApplicationServices;
using Volunteering.Data.ViewModels;

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

        [HttpPost("add")]
        [ProducesResponseType(typeof(NewsVM), 200)]
        [Consumes("multipart/form-data")]
        public IActionResult Add([FromForm]NewsVM vm)
        {
            return Ok(_service.Add(vm));
        }
    }
}
