using ApplicationCore.Entities.Config;
using System.Collections.Generic;

namespace ApplicationCore.Interfaces.Auth
{
    public interface IJwt
    {
        List<string> GetJwt(ClientApi clientApi);
    }
}
