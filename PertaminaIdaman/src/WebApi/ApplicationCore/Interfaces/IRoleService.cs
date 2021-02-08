using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Config
{
    public interface IRoleService
    {
        Task<IReadOnlyList<Role>> GetAllAsync();
    }
}
