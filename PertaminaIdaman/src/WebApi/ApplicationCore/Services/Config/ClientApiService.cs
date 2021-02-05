using ApplicationCore.Entities.Config;
using ApplicationCore.Helpers;
using ApplicationCore.Interfaces.BaseEntity;
using ApplicationCore.Interfaces.Config;
using ApplicationCore.Specifications.Config;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Config
{
    public class ClientApiService : IClientApiService
    {
        private readonly IEfRepository<ClientApi, Guid> _repository;
        private readonly IConfiguration _configuration;

        public ClientApiService(IEfRepository<ClientApi, Guid> repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<IReadOnlyList<ClientApi>> GetAllAsync()
        {
            var spec = new ClientApiSpecification();
            var data = await _repository.GetAllAsync(spec);

            foreach (var item in data)
            {
                item.ClientSecret = item.ClientSecret.ToBase64EncodeWithKey(_configuration["Security:EncryptKey"]);
            }

            return data;
        }
    }
}
