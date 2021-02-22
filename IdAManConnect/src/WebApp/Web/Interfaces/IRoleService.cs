using System.Collections.Generic;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleAppViewModel>> GetAllAsync();
        Task<string> CreateAsync(RoleAppViewModel roleAppViewModel);
    }
}