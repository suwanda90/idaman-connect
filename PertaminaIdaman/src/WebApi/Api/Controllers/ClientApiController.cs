using ApplicationCore.Interfaces.Config;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class ClientApiController : ControllerBase
    {
        private readonly IClientApiService _service;

        public ClientApiController(IClientApiService service)
        {
            _service = service;
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }
    }
}
