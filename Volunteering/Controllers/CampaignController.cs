using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volunteering.ApplicationServices;
using Volunteering.Data.Models;
using Volunteering.Data.ViewModels;
using Volunteering.Helpers;

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
        public IActionResult GetAll([FromQuery]CampaignFilter ?filter)
        {
            return Ok(_service.GetAll(filter));
        }

        [HttpGet("get-new"), Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<CampaignVM>), 200)]
        public IActionResult GetNew()
        {
            return Ok(_service.GetNew());
        }

        [HttpPatch("update-status"), Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult UpdateStatus([FromBody]CampaignStatusUpdateRequest req)
        {
            
            if (ModelState.IsValid)
            {
                var res = _service.UpdateStatus(req);
                if(res)
                {
                    return Ok(res);
                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}
