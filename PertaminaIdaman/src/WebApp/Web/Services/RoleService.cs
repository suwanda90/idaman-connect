using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Web.Helpers;
using Web.Interfaces;
using Web.ViewModels;
using Web.ViewModels.Idaman;

namespace Web.Services
{
    [Authorize(Policy = "Bearer")]
    public class RoleService : IRoleService
    {
        private readonly HttpClient _clientLogin;
        private readonly HttpClient _clientApi;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettingsViewModel _appSettingsViewModel;

        public RoleService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, AppSettingsViewModel appSettingsViewModel)
        {
            _httpContextAccessor = httpContextAccessor;
            _appSettingsViewModel = appSettingsViewModel;
            _config = configuration;

            _clientLogin = new HttpClient
            {
                BaseAddress = new Uri(_config["Idaman:UrlLogin"])
            };

            var model = new TokenIdamanViewModel
            {
                Scope = _config["Idaman:Scope"].Trim().Replace(",", " "),
                ClientId = _config["Idaman:ClientId"],
                ClientSecret = _config["Idaman:ClientSecret"],
                GrantType = "client_credentials"
            };

            var token = BaseApiHelper.GetTokenIdaman(_clientLogin, model, _httpContextAccessor, _appSettingsViewModel);

            _clientApi = new HttpClient
            {
                BaseAddress = new Uri(_config["Api:Url"])
            };

            _clientApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<List<RoleAppViewModel>> GetAllAsync()
        {
            var response = await _clientApi.GetAsync("Role/get/all");
            var contents = await response.Content.ReadAsStringAsync();
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<RoleAppViewModel>>(contents);
            }

            return new List<RoleAppViewModel>();
        }
    }
}
