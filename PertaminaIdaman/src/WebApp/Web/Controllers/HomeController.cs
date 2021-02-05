using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Helpers;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IIdamanService _idamanService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettingsViewModel _appSettings;

        public HomeController(IMemoryCache memoryCache, IIdamanService idamanService, IHttpContextAccessor httpContextAccessor, AppSettingsViewModel appSettings)
        {
            _memoryCache = memoryCache;
            _idamanService = idamanService;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ProfileAsync()
        {
            var userIdaman = await _idamanService.GetUsersAsync();
            ViewBag.RoleName = await _idamanService.GetRoleNameAsync();
            ViewBag.TenantCode = await _idamanService.GetTenantCodeAsync();
            return View(userIdaman.Data);
        }

        public async Task<IActionResult> Logout()
        {
            CookieHelper.RemoveCookie(SecurityHelper.ToBase64Encode(_appSettings.ApplicationCookiesName + ".RoleName"), _httpContextAccessor);
            CookieHelper.RemoveCookie(SecurityHelper.ToBase64Encode(_appSettings.ApplicationCookiesName + ".User"), _httpContextAccessor);
            CookieHelper.RemoveCookie(SecurityHelper.ToBase64Encode(_appSettings.ApplicationCookiesName + ".Tenant"), _httpContextAccessor);
            CookieHelper.RemoveCookie(SecurityHelper.ToBase64Encode(_appSettings.ApplicationCookiesName + ".Idaman.Token"), _httpContextAccessor);

            ICollection<string> myCookies = _httpContextAccessor.HttpContext.Request.Cookies.Keys;
            foreach (string cookie in myCookies)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookie);
            }

            await HttpContext.SignOutAsync();
            return SignOut("Cookies", "oidc");
        }
    }
}