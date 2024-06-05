using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volunteering.ApplicationServices;
using Volunteering.Data.DomainServices;
using Volunteering.Data.ViewModels;
using Volunteering.Helpers;

namespace Volunteering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        public DonationApplicationService _service;
        public DonationController(DonationApplicationService service)
        {
            _service = service;
        }

        [HttpPost("add"), AllowAnonymous]
        [ProducesResponseType(typeof(Response<DonationVM>), 200)]
        public IActionResult Add([FromBody] DonationVM vm)
        {
            if (ModelState.IsValid)
            {
                Guid? userId = null;
                try
                {
                    userId = Guid.Parse(HttpContext.User.FindFirst("UserId")?.Value);
                }
                catch (Exception ex)
                {

                    userId = null;
                }
                vm.UserId = userId;

                var response = _service.Add(vm);
                if (!response.IsSuccess)
                {
                    return BadRequest(response.Error);
                }

                
                var donationId = response.Data.donationId;
                
                string uri = Url.Action("GetById", new { id = donationId });
                
                return Created(uri, response.Data);
            }
            return BadRequest();
        }


        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(DonationVM), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(Guid id)
        {
            var donation = _service.GetById(id);
            if (donation == null)
            {
                return NotFound();
            }
            return Ok(donation);
        }


        [HttpGet("get-by-user-id"), Authorize(Roles ="Registered, Admin")]
        [ProducesResponseType(typeof(List<DonationVM>), 200)]
        public IActionResult GetByUserId()
        {
            if (ModelState.IsValid)
            {
                Guid userId = Guid.Parse(HttpContext.User.FindFirst("UserId")?.Value);
                var response = _service.GetByUserId(userId);

                if (!response.IsSuccess)
                {
                    return BadRequest(response.Error);
                }
                return Ok(response.Data);
            }
            return BadRequest();
        }
    }
}
