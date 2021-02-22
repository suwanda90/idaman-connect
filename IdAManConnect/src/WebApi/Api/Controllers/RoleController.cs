using ApplicationCore.Entities;
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
        [Authorize(Policy = "role.readAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpPost]
        [Authorize(Policy = "role.create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromBody] Role data)
        {
            return Ok("OK");
        }
    }
}
