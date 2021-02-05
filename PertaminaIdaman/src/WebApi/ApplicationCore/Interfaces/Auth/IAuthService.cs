using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<List<string>> GenerateJwtAsync(string clientId, string clientSecret);
    }
}
