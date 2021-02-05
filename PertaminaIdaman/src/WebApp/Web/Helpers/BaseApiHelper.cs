using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using Web.ViewModels;
using Web.ViewModels.Idaman;

namespace Web.Helpers
{
    public static class BaseApiHelper
    {
        private static string token;
        private static DateTime tokenExpiry;

        public static string GetToken(HttpClient client, string clientId, string clientSecret)
        {
            if (token != null && ((tokenExpiry - DateTime.Now).TotalSeconds > 60))
            {
                return token;
            }

            var url = "Auth/token/" + clientId + "/" + clientSecret;

            var response = client.GetAsync(url).GetAwaiter().GetResult();

            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var tokens = JsonConvert.DeserializeObject<IDictionary<string, object>>(content);
            token = tokens["token"] as string;
            tokenExpiry = DateTime.Parse(tokens["validTo"].ToString());

            return token;
        }

        public static string GetTokenIdaman(HttpClient client, TokenIdamanViewModel model, IHttpContextAccessor httpContextAccessor, AppSettingsViewModel appSettingsViewModel)
        {
            string token;

            if (!CookieHelper.IsExistCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Idaman.Token"), httpContextAccessor))
            {
                token = GenerateTokenIdaman(client, model, httpContextAccessor, appSettingsViewModel);
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
                token = GenerateTokenIdaman(client, model, httpContextAccessor, appSettingsViewModel);
            }

            return token;
        }

        private static string GenerateTokenIdaman(HttpClient client, TokenIdamanViewModel model, IHttpContextAccessor httpContextAccessor, AppSettingsViewModel appSettingsViewModel)
        {
            var multiContent = new MultipartFormDataContent
            {
                { new StringContent(model.ClientId), "client_id" },
                { new StringContent(model.ClientSecret), "client_secret" },
                { new StringContent(model.Scope), "scope" },
                { new StringContent(model.GrantType), "grant_type" }
            };

            var response = client.PostAsync("connect/token", multiContent).GetAwaiter().GetResult();
            var contents = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var tokens = JsonConvert.DeserializeObject<IDictionary<string, object>>(contents);
            var token = tokens["access_token"] as string;
            CookieHelper.SetCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Idaman.Token"), token.ToBase64Encode(), 30, httpContextAccessor);

            return token;
        }
    }
}

