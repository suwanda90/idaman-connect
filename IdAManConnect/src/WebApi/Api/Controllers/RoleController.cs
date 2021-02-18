using ApplicationCore.Interfaces.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet("get/all")]
        [Authorize(Policy = "role.ReadAll")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }
    }
}
