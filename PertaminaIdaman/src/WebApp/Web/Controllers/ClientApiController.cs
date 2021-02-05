using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Interfaces;

namespace Web.Controllers
{
    public class ClientApiController : Controller
    {
        private readonly IClientApiService _clientApiService;

        public ClientApiController(IClientApiService clientApiService)
        {
            _clientApiService = clientApiService;
        }

        [Authorize]
        public async Task<IActionResult> IndexAsync()
        {
            var clientApis = await _clientApiService.GetAllAsync();
            return View(clientApis);
        }
    }
}