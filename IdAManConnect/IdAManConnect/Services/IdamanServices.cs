using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdAManConnect.Helpers;
using IdAManConnect.Interfaces;
using IdAManConnect.ViewModels;
using IdAManConnect.ViewModels.Helpers;
using IdAManConnect.ViewModels.Idaman;

namespace IdAManConnect.Services
{
    [Authorize(Policy = "Bearer")]
    public class IdamanService : IIdamanService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static string email;
        private readonly AppSettingsViewModel _appSettings;

        public IdamanService(IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            AppSettingsViewModel appSettings
        )
        {
            _config = configuration;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings;

            _client = AuthHelper.ClientIdamanBearear(_httpContextAccessor, _appSettings);
            email = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        }

        public async Task<BaseApiResultViewModel<UserIdamanViewModel>> GetUsersAsync()
        {
            if (CookieHelper.IsExistCookie(SecurityHelper.ToBase64Encode(_appSettings.ApplicationCookiesName + ".User"), _httpContextAccessor))
            {
                var user = CookieHelper.GetCookie(SecurityHelper.ToBase64Encode(_appSettings.ApplicationCookiesName + ".User"), _httpContextAccessor).ToBase64Decode();
                var result = new BaseApiResultViewModel<UserIdamanViewModel>
                {
                    Data = JsonConvert.DeserializeObject<UserIdamanViewModel>(user),
                    StatusCode = 200,
                    Message = "Success get data from cookie"
                };

                return result;
            }
            else
            {
                var response = await _client.GetAsync("Users/" + email);
                var result = new BaseApiResultViewModel<UserIdamanViewModel>
                {
                    Data = new UserIdamanViewModel(),
                    StatusCode = (int)response.StatusCode,
                    Message = response.StatusCode.ToString()
                };

                if (result.StatusCode == 200)
                {
                    var contents = await response.Content.ReadAsStringAsync();
                    result.Data = JsonConvert.DeserializeObject<UserIdamanViewModel>(contents);
                }

                CookieHelper.SetCookie(SecurityHelper.ToBase64Encode(_appSettings.ApplicationCookiesName + ".User"), JsonConvert.SerializeObject(result.Data).ToBase64Encode(), 30, _httpContextAccessor);

                return result;
            }
        }

        public async Task<string> GetTenantCodeAsync()
        {
            var tenantCode = string.Empty;
            var user = await GetUsersAsync();
            if (user.Data.ExtensionAttributes != null)
            {
                var extensionAttributes = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(user.Data.ExtensionAttributes.ToString());
                foreach (var extensionAttribute in extensionAttributes)
                {
                    foreach (var item in extensionAttribute.Value)
                    {
                        if (item.Key.ToLower() == "tenant")
                        {
                            tenantCode = item.Value;
                            break;
                        }
                    }
                }
            }

            return tenantCode;
        }

        public async Task<string> GetRoleNameAsync()
        {
            string roleName;

            if (CookieHelper.IsExistCookie(SecurityHelper.ToBase64Encode(_appSettings.ApplicationCookiesName + ".RoleName"), _httpContextAccessor))
            {
                roleName = CookieHelper.GetCookie(SecurityHelper.ToBase64Encode(_appSettings.ApplicationCookiesName + ".RoleName"), _httpContextAccessor).ToBase64Decode();
            }
            else
            {
                var user = await GetUsersAsync();
                roleName = await GetRoleAsync(user.Data.UserId);
                CookieHelper.SetCookie(SecurityHelper.ToBase64Encode(_appSettings.ApplicationCookiesName + ".RoleName"), roleName.ToBase64Encode(), 30, _httpContextAccessor);
            }

            return roleName;
        }

        public async Task<string> GetRoleAsync(string userIdamanId)
        {
            var roleName = string.Empty;

            var applications = await GetApplicationsAsync(userIdamanId);
            if (applications.Data.Count > 0)
            {
                var application = applications.Data.FirstOrDefault(x => x.ClientId == _config["Idaman:ClientId"] && x.ClientSecret == _config["Idaman:ClientSecret"]);
                if (application != null)
                {
                    var applicationRoles = await GetApplicationRolesAsync(application.Id);
                    if (applicationRoles.Data.Count > 0)
                    {
                        var whitelists = await GetWhitelistsAsync(userIdamanId);
                        if (whitelists.Data.Count > 0)
                        {
                            foreach (var item in whitelists.Data)
                            {
                                var whitelist = await GetWhitelistDetailAsync(item.Id);
                                var whitelistRole = whitelist.Data.Role.FirstOrDefault(x => x.Application.Id == application.Id);
                                if (whitelistRole != null)
                                {
                                    roleName = whitelistRole.RoleName;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return roleName;
        }

        public async Task<BaseApiResultViewModel<List<ApplicationIdamanViewModel>>> GetApplicationsAsync(string userIdamanId)
        {
            var response = await _client.GetAsync("Applications/MyApps?userId=" + userIdamanId);
            var result = new BaseApiResultViewModel<List<ApplicationIdamanViewModel>>
            {
                Data = new List<ApplicationIdamanViewModel>(),
                StatusCode = (int)response.StatusCode,
                Message = response.StatusCode.ToString()
            };

            if (result.StatusCode == 200)
            {
                var contents = await response.Content.ReadAsStringAsync();
                var results = JsonConvert.DeserializeObject<IDictionary<string, object>>(contents);
                var applications = results["value"];
                result.Data = JsonConvert.DeserializeObject<List<ApplicationIdamanViewModel>>(applications.ToString());
            }

            return result;
        }

        public async Task<BaseApiResultViewModel<List<ApplicationRoleIdamanViewModel>>> GetApplicationRolesAsync(string applicationId)
        {
            var response = await _client.GetAsync("Applications/Roles/" + applicationId);
            var result = new BaseApiResultViewModel<List<ApplicationRoleIdamanViewModel>>
            {
                Data = new List<ApplicationRoleIdamanViewModel>(),
                StatusCode = (int)response.StatusCode,
                Message = response.StatusCode.ToString()
            };

            if (result.StatusCode == 200)
            {
                var contents = await response.Content.ReadAsStringAsync();
                var results = JsonConvert.DeserializeObject<IDictionary<string, object>>(contents);
                var roles = results["value"];
                result.Data = JsonConvert.DeserializeObject<List<ApplicationRoleIdamanViewModel>>(roles.ToString());
            }

            return result;
        }

        public async Task<BaseApiResultViewModel<List<WhitelistViewModel>>> GetWhitelistsAsync(string userIdamanId)
        {
            var response = await _client.GetAsync("Users/Whitelist/" + userIdamanId);
            var result = new BaseApiResultViewModel<List<WhitelistViewModel>>
            {
                Data = new List<WhitelistViewModel>(),
                StatusCode = (int)response.StatusCode,
                Message = response.StatusCode.ToString()
            };

            if (result.StatusCode == 200)
            {
                var contents = await response.Content.ReadAsStringAsync();
                var results = JsonConvert.DeserializeObject<IDictionary<string, object>>(contents);
                var whitelists = results["value"];
                result.Data = JsonConvert.DeserializeObject<List<WhitelistViewModel>>(whitelists.ToString());
            }

            return result;
        }

        public async Task<BaseApiResultViewModel<WhitelistViewModel>> GetWhitelistDetailAsync(string whitelistId)
        {
            var response = await _client.GetAsync("Users/Whitelist/Detail/" + whitelistId);
            var result = new BaseApiResultViewModel<WhitelistViewModel>
            {
                Data = new WhitelistViewModel(),
                StatusCode = (int)response.StatusCode,
                Message = response.StatusCode.ToString()
            };

            if (result.StatusCode == 200)
            {
                var contents = await response.Content.ReadAsStringAsync();
                result.Data = JsonConvert.DeserializeObject<WhitelistViewModel>(contents);
            }

            return result;
        }

        public async Task<BaseApiResultViewModel<UserIdamanViewModel>> GetUsersAsync(string username, string roleName)
        {
            var response = await _client.GetAsync("Users/" + username);
            var result = new BaseApiResultViewModel<UserIdamanViewModel>
            {
                Data = new UserIdamanViewModel(),
                StatusCode = (int)response.StatusCode,
                Message = response.StatusCode.ToString()
            };

            if (result.StatusCode == 200)
            {
                var contents = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<UserIdamanViewModel>(contents);

                var role = await GetRoleAsync(data.UserId);
                if (!string.IsNullOrEmpty(role))
                {
                    if (role.ToLower() == roleName.ToLower())
                    {
                        result.Data = data;
                    }
                }
            }

            return result;
        }
    }
}
