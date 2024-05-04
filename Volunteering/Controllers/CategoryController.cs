using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volunteering.ApplicationServices;
using Volunteering.Data.ViewModels;

namespace Volunteering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private CategoryApplicationService _service;

        public CategoryController(CategoryApplicationService service)
        {
            _service = service;
        }

        [HttpGet("get-all"), AllowAnonymous]
        [ProducesResponseType(typeof(List<CategoryVM>), 200)]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost("add"), Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CategoryVM), 200)]
        public IActionResult Add([FromBody] CategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                return Ok(_service.Add(vm));
            }
            return BadRequest();
        }

        [HttpPut("update"), Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CategoryVM), 200)]
        public IActionResult Update([FromBody] CategoryVM vm)
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
        private IActionResult Delete([FromBody] CategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                var res = _service.Delete(vm);
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
