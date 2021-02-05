using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class GatewayTimeout : BaseApiResponse
    {
        public GatewayTimeout(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.GatewayTimeout, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.GatewayTimeout), message, data);
        }
    }
}
