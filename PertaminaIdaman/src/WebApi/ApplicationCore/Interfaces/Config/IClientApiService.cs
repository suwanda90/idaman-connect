using ApplicationCore.Entities.Config;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Config
{
    public interface IClientApiService
    {
        Task<IReadOnlyList<ClientApi>> GetAllAsync();
    }
}
