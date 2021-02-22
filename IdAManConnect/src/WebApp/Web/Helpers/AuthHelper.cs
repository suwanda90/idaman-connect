using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using Web.ViewModels;
using Web.ViewModels.Idaman;

namespace Web.Helpers
{
    public static class AuthHelper
    {
        public static HttpClient ClientBearear(IHttpContextAccessor httpContextAccessor, AppSettingsViewModel appSettingsViewModel)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(appSettingsViewModel.ApiUrl)
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetTokenIdaman(client, httpContextAccessor, appSettingsViewModel));

            return client;
        }

        public static HttpClient ClientIdamanBearear(IHttpContextAccessor httpContextAccessor, AppSettingsViewModel appSettingsViewModel)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(appSettingsViewModel.IdamanUrlApi)
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetTokenIdaman(client, httpContextAccessor, appSettingsViewModel));

            return client;
        }

        public static string GetTokenIdaman(HttpClient client, IHttpContextAccessor httpContextAccessor, AppSettingsViewModel appSettingsViewModel)
        {
            string token;

            if (!CookieHelper.IsExistCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Idaman.Token"), httpContextAccessor))
            {
                token = GenerateTokenIdaman(client, httpContextAccessor, appSettingsViewModel);
            }
            else
            {
                token = CookieHelper.GetCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Idaman.Token"), httpContextAccessor).ToBase64Decode();
            }

            var handler = new JwtSecurityTokenHandler();
            var tokenSecurity = handler.ReadToken(token) as JwtSecurityToken;
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(tokenSecurity.Payload.Exp.Value);
            if (!string.IsNullOrEmpty(token) && ((dateTimeOffset.LocalDateTime - DateTimeOffset.Now).TotalSeconds < 60))
            {
                token = GenerateTokenIdaman(client, httpContextAccessor, appSettingsViewModel);
            }

            return token;
        }

        private static string GenerateTokenIdaman(HttpClient client, IHttpContextAccessor httpContextAccessor, AppSettingsViewModel appSettingsViewModel)
        {
            if (client != null)
            {
                client = new HttpClient
                {
                    BaseAddress = new Uri(appSettingsViewModel.IdamanUrlLogin)
                };

                var model = new TokenIdamanViewModel
                {
                    Scopes = appSettingsViewModel.IdamanScopes.Trim().Replace(" ", "").Replace(",", " "),
                    ClientId = appSettingsViewModel.IdamanClientId,
                    ClientSecret = appSettingsViewModel.IdamanClientSecret,
                    GrantType = "client_credentials"
                };

                var multiContent = new MultipartFormDataContent
            {
                { new StringContent(model.ClientId), "client_id" },
                { new StringContent(model.ClientSecret), "client_secret" },
                { new StringContent(model.Scopes), "scope" },
                { new StringContent(model.GrantType), "grant_type" }
            };

                var response = client.PostAsync("connect/token", multiContent).GetAwaiter().GetResult();
                var contents = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var tokens = JsonConvert.DeserializeObject<IDictionary<string, object>>(contents);
                var token = tokens["access_token"] as string;
                CookieHelper.SetCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Idaman.Token"), token.ToBase64Encode(), 30, httpContextAccessor);

                return token;
            }

            return string.Empty;
        }

        public static HttpClient ClientApiBearear(IHttpContextAccessor httpContextAccessor, AppSettingsViewModel appSettingsViewModel)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(appSettingsViewModel.ApiUrl)
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetTokenApiIdaman(client, httpContextAccessor, appSettingsViewModel));

            return client;
        }

        public static string GetTokenApiIdaman(HttpClient client, IHttpContextAccessor httpContextAccessor, AppSettingsViewModel appSettingsViewModel)
        {
            string token;

            if (!CookieHelper.IsExistCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Idaman.TokenApi"), httpContextAccessor))
            {
                token = GenerateTokenApiIdaman(client, httpContextAccessor, appSettingsViewModel);
            }
            else
            {
                token = CookieHelper.GetCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Idaman.TokenApi"), httpContextAccessor).ToBase64Decode();
            }

            var handler = new JwtSecurityTokenHandler();
            var tokenSecurity = handler.ReadToken(token) as JwtSecurityToken;
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(tokenSecurity.Payload.Exp.Value);
            if (!string.IsNullOrEmpty(token) && ((dateTimeOffset.LocalDateTime - DateTimeOffset.Now).TotalSeconds < 60))
            {
                token = GenerateTokenApiIdaman(client, httpContextAccessor, appSettingsViewModel);
            }

            return token;
        }

        private static string GenerateTokenApiIdaman(HttpClient client, IHttpContextAccessor httpContextAccessor, AppSettingsViewModel appSettingsViewModel)
        {
            if (client != null)
            {
                client = new HttpClient
                {
                    BaseAddress = new Uri(appSettingsViewModel.IdamanUrlLogin)
                };

                var model = new TokenIdamanViewModel
                {
                    IdamanConnectApiScopes = appSettingsViewModel.IdamanConnectApiScopes.Replace(" ", "").Replace(",", " "),
                    IdamanConnectApiClientId = appSettingsViewModel.IdamanConnectApiClientId,
                    IdamanConnectApiClientSecret = appSettingsViewModel.IdamanConnectApiClientSecret,
                    IdamanConnectApiGrantType = "client_credentials"
                };

                var apiScopes = string.Empty;

                var scopes = model.IdamanConnectApiScopes.Split(" ");

                foreach (var scope in scopes)
                {
                    if (scope == "api.auth")
                    {
                        apiScopes += scope + " ";
                    }
                    else
                    {
                        apiScopes += "api://" + appSettingsViewModel.IdamanConnectApiObjectId + "/" + scope + " ";
                    }
                }

                var multiContent = new MultipartFormDataContent
                {
                    { new StringContent(model.IdamanConnectApiClientId), "client_id" },
                    { new StringContent(model.IdamanConnectApiClientSecret), "client_secret" },
                    { new StringContent(apiScopes.TrimEnd()), "scope" },
                    { new StringContent(model.IdamanConnectApiGrantType), "grant_type" }
                };

                var response = client.PostAsync("connect/token", multiContent).GetAwaiter().GetResult();
                var contents = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var tokens = JsonConvert.DeserializeObject<IDictionary<string, object>>(contents);
                var token = tokens["access_token"] as string;
                CookieHelper.SetCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Idaman.TokenApi"), token.ToBase64Encode(), 30, httpContextAccessor);

                return token;
            }

            return string.Empty;
        }
    }
}

