using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volunteering.ApplicationServices;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;

namespace Volunteering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        public CampaignApplicationService _service;
        public CampaignController(CampaignApplicationService service)
        {
            _service = service;
        }

        [HttpPost("add"), Authorize(Roles = "Registered")]
        [ProducesResponseType(typeof(CampaignVM), 200)]
        [Consumes("multipart/form-data")]
        public IActionResult Add([FromForm] CampaignVM vm)
        {
            Guid userId = Guid.Parse(HttpContext.User.FindFirst("UserId")?.Value);
            var response = _service.Add(userId, vm);
            if (ModelState.IsValid)
            {
                if (!response.IsSuccess)
                {
                    return BadRequest(response.Error);
                }
                return Ok(response.Data);
            }

            return BadRequest();
        }

        [HttpGet("get-all"), AllowAnonymous]
        [ProducesResponseType(typeof(List<CampaignVM>), 200)]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }
    }
}
