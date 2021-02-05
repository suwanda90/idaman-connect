using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApplicationCore.Entities.Config;
using ApplicationCore.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    [Authorize(Policy = "Bearer")]
    public class Jwt : IJwt
    {
        private readonly IConfiguration config;
        public Jwt(IConfiguration configuration)
        {
            config = configuration;
        }

        public List<string> GetJwt(ClientApi clientApi)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, clientApi.Name),
                new Claim(JwtRegisteredClaimNames.UniqueName, clientApi.ClientId),
                new Claim(JwtRegisteredClaimNames.Sid, clientApi.ClientSecret)
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"], //Owner Api => Application Name or URL API
                audience: clientApi.ClientId, //Client Token => Client Name or URL Website
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(int.Parse(config["Jwt:ExpireInMinutes"])),
                signingCredentials: credentials);

            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);

            var tokens = new List<string>
            {
                encodeToken,
                token.ValidFrom.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"),
                token.ValidTo.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")
            };

            return tokens;
        }
    }
}
