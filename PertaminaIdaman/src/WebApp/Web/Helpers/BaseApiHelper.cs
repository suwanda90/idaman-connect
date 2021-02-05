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
        public static string GetToken(HttpClient client, string clientId, string clientSecret, IHttpContextAccessor httpContextAccessor, AppSettingsViewModel appSettingsViewModel)
        {
            var tokenExpiry = string.Empty;
            string token;

            if (!CookieHelper.IsExistCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Api.Token"), httpContextAccessor) &&
                !CookieHelper.IsExistCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Api.TokenExpiry"), httpContextAccessor))
            {
                token = GenerateToken(client, clientId, clientSecret, httpContextAccessor, appSettingsViewModel);
            }
            else
            {
                token = CookieHelper.GetCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Api.Token"), httpContextAccessor).ToBase64Decode();
                tokenExpiry = CookieHelper.GetCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Api.TokenExpiry"), httpContextAccessor).ToBase64Decode();
            }

            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(tokenExpiry))
            {
                var dateTimeOffset = DateTime.Parse(tokenExpiry);
                if ((dateTimeOffset - DateTime.Now).TotalSeconds < 60)
                {
                    token = GenerateToken(client, clientId, clientSecret, httpContextAccessor, appSettingsViewModel);
                }
            }

            return token;
        }

        private static string GenerateToken(HttpClient client, string clientId, string clientSecret, IHttpContextAccessor httpContextAccessor, AppSettingsViewModel appSettingsViewModel)
        {
            var url = "Auth/token/" + clientId + "/" + clientSecret;

            var response = client.GetAsync(url).GetAwaiter().GetResult();

            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var tokens = JsonConvert.DeserializeObject<IDictionary<string, object>>(content);
            var token = tokens["token"] as string;
            var tokenExpiry = tokens["validTo"] as string;

            CookieHelper.SetCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Api.Token"), token.ToBase64Encode(), 30, httpContextAccessor);
            CookieHelper.SetCookie(SecurityHelper.ToBase64Encode(appSettingsViewModel.ApplicationCookiesName + ".Api.TokenExpiry"), tokenExpiry.ToBase64Encode(), 30, httpContextAccessor);

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

