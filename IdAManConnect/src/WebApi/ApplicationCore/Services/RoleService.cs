using ApplicationCore.Entities;
using ApplicationCore.Interfaces.BaseEntity;
using ApplicationCore.Interfaces.Config;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Config
{
    public class RoleService : IRoleService
    {
        private readonly IEfRepository<Role, Guid> _repository;

        public RoleService(IEfRepository<Role, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<Role>> GetAllAsync()
        {
            var spec = new RoleSpecification();
            return await _repository.GetAllAsync(spec);
        }
    }
}
