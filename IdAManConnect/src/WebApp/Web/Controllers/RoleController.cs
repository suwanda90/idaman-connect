using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Interfaces;

namespace Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize]
        public async Task<IActionResult> IndexAsync()
        {
            var roles = await _roleService.GetAllAsync();
            return View(roles);
        }
    }
}