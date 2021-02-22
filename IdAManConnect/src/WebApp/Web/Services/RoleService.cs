using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Web.Helpers;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    [Authorize(Policy = "Bearer")]
    public class RoleService : IRoleService
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettingsViewModel _appSettingsViewModel;

        public RoleService(IHttpContextAccessor httpContextAccessor, AppSettingsViewModel appSettingsViewModel)
        {
            _httpContextAccessor = httpContextAccessor;
            _appSettingsViewModel = appSettingsViewModel;

            _client = AuthHelper.ClientApiBearear(_httpContextAccessor, _appSettingsViewModel);
        }

        public async Task<List<RoleAppViewModel>> GetAllAsync()
        {
            var response = await _client.GetAsync("Role/get/all");
            var contents = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<RoleAppViewModel>>(contents);
            }

            return new List<RoleAppViewModel>();
        }

        public async Task<string> CreateAsync(RoleAppViewModel param)
        {
            var response = await _client.PostAsync("Role", new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json"));
            var contents = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<string>(contents);
            }

            return string.Empty;
        }
    }
}
