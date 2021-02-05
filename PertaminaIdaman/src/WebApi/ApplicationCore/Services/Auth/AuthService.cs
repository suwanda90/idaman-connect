using ApplicationCore.Entities.Config;
using ApplicationCore.Interfaces.Auth;
using ApplicationCore.Interfaces.BaseEntity;
using ApplicationCore.Specifications.Config;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IEfRepository<ClientApi, Guid> _clientApiRepository;
        private readonly IJwt _jwt;

        public AuthService(IEfRepository<ClientApi, Guid> clientApiRepository, IJwt jwt)
        {
            _clientApiRepository = clientApiRepository;
            _jwt = jwt;
        }

        public async Task<List<string>> GenerateJwtAsync(string clientId, string clientSecret)
        {
            var tokens = new List<string>();
            var spec = new ClientApiSpecification(clientId, clientSecret);
            var clientApi = await _clientApiRepository.GetAsync(spec);

            if (clientApi != null)
            {
                tokens = _jwt.GetJwt(clientApi);

                //update ClientApi
                clientApi.Token = tokens[0];
                clientApi.ExpiredToken = DateTime.Parse(tokens[2]);
                await _clientApiRepository.UpdateAsync(clientApi);
            }

            return tokens;
        }
    }
}
