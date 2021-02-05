using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class ProxyAuthenticationRequired : BaseApiResponse
    {
        public ProxyAuthenticationRequired(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.ProxyAuthenticationRequired, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.ProxyAuthenticationRequired), message, data);
        }
    }
}
