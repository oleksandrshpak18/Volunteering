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
        public IActionResult GetAll([FromQuery]CampaignFilter ?filter, string? sortBy = "CreateDate", bool isDescending=true, int page=1, int pageSize=8)
        {
            return Ok(_service.GetAll(filter, sortBy, isDescending, page, pageSize));
        }

        [HttpGet("get"), AllowAnonymous]
        [ProducesResponseType(typeof(CampaignVM), 200)]
        public IActionResult GetById([FromQuery] Guid id)
        {
            return Ok(_service.GetById(id));
        }

        [HttpGet("get-by-user-id"), AllowAnonymous]
        [ProducesResponseType(typeof(CampaignVM), 200)]
        public IActionResult GetByUserId([FromQuery] Guid userId)
        {
            return Ok(_service.GetByUserId(userId));
        }

        [HttpGet("get-recent"), AllowAnonymous]
        [ProducesResponseType(typeof(List<CampaignVM>), 200)]
        public IActionResult GetRecent([FromQuery] int count = 6)
        {
            return Ok(_service.GetRecent(count));
        }

        [HttpGet("get-statistics"), AllowAnonymous]
        [ProducesResponseType(typeof(StatisticsResponse), 200)]
        public IActionResult GetStatistics()
        {
            return Ok(_service.GetStatistics());
        }

        [HttpGet("get-new"), Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<CampaignVM>), 200)]
        public IActionResult GetNew([FromQuery] int page=1)
        {
            return Ok(_service.GetNew(page));
        }

        [HttpPatch("update-status"), Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult UpdateStatus([FromBody]CampaignStatusUpdateRequest req)
        {
            
            if (ModelState.IsValid)
            {
                var res = _service.UpdateStatus(req);
                if(res != null)
                {
                    return Ok(res);
                }
                return NotFound();
            }
            return BadRequest();
        }

        [HttpPatch("add-report"), Authorize(Roles = "Registered")]
        [ProducesResponseType(typeof(CampaignVM), 200)]
        [Consumes("multipart/form-data")]
        public IActionResult AddReport([FromForm] ReportVM vm)
        {

            if (ModelState.IsValid)
            {
                Guid userId = Guid.Parse(HttpContext.User.FindFirst("UserId")?.Value);

                var res = _service.AddReport(userId, vm);
                if (res != null)
                {
                    return Ok(res);
                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}
