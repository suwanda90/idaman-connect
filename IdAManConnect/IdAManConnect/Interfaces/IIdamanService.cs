using System.Collections.Generic;
using System.Threading.Tasks;
using IdAManConnect.ViewModels.Helpers;
using IdAManConnect.ViewModels.Idaman;

namespace IdAManConnect.Interfaces
{
    public interface IIdamanService
    {
        Task<string> GetRoleNameAsync();
        Task<BaseApiResultViewModel<List<ApplicationIdamanViewModel>>> GetApplicationsAsync(string userIdamanId);
        Task<BaseApiResultViewModel<List<ApplicationRoleIdamanViewModel>>> GetApplicationRolesAsync(string applicationId);
        Task<BaseApiResultViewModel<List<WhitelistViewModel>>> GetWhitelistsAsync(string userIdamanId);
        Task<BaseApiResultViewModel<WhitelistViewModel>> GetWhitelistDetailAsync(string whitelistId);
        Task<BaseApiResultViewModel<UserIdamanViewModel>> GetUsersAsync();
        Task<string> GetTenantCodeAsync();
        Task<BaseApiResultViewModel<UserIdamanViewModel>> GetUsersAsync(string username, string roleName);
    }
}
