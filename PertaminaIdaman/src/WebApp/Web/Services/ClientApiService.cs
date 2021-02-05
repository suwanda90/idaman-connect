using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Web.Helpers;
using Web.Interfaces;
using Web.ViewModels;
using Web.ViewModels.Helpers;

namespace Web.Services
{
    [Authorize(Policy = "Bearer")]
    public class ClientApiService : IClientApiService
    {
        private readonly HttpClient client;
        private readonly IConfiguration config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettingsViewModel _appSettingsViewModel;

        public ClientApiService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, AppSettingsViewModel appSettingsViewModel)
        {
            _httpContextAccessor = httpContextAccessor;
            _appSettingsViewModel = appSettingsViewModel;
            config = configuration;

            client = new HttpClient
            {
                BaseAddress = new Uri(config["Api:Url"])
            };
            client.Timeout = TimeSpan.FromMinutes(180);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BaseApiHelper.GetToken(client, config["Api:ClientId"], config["Api:ClientSecret"], _httpContextAccessor, _appSettingsViewModel));
        }


        public async Task<List<ClientApiViewModel>> GetAllAsync()
        {
            var response = await client.GetAsync("ClientApi/get/all");
            var contents = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ClientApiViewModel>>(contents);
        }
    }
}
