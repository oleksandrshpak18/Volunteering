using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volunteering.ApplicationServices;
using Volunteering.Data.ViewModels;

namespace Volunteering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignPriorityController : ControllerBase
    {
        public CampaignPriorityApplicationService _service;
        public CampaignPriorityController(CampaignPriorityApplicationService service)
        {
            _service = service;
        }

        [HttpGet("get-all"), AllowAnonymous]
        [ProducesResponseType(typeof(List<CampaignPriorityVm>), 200)]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost("add"), Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CampaignPriorityVm), 200)]
        public IActionResult Add([FromBody] CampaignPriorityVm vm)
        { 
            if (ModelState.IsValid)
            {
                return Ok(_service.Add(vm));
            }
            return BadRequest();
        }

        [HttpPut("update"), Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CampaignPriorityVm), 200)]
        public IActionResult Update([FromBody] CampaignPriorityVm vm)
        {
            if (ModelState.IsValid)
            { 
                var res = _service.Update(vm);
                if (res != null)
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
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult Delete([FromQuery] Guid id)
        {
            if (ModelState.IsValid)
            {
                var res = _service.Delete(id);
                if (res)
                {
                    return NoContent();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }
    }
}
