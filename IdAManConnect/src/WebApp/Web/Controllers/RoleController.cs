using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.ViewModels;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(RoleAppViewModel roleAppViewModel)
        {
            var roles = await _roleService.CreateAsync(roleAppViewModel);
            return View(roles);
        }
    }
}