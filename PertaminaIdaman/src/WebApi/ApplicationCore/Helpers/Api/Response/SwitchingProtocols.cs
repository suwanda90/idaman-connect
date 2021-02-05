using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class SwitchingProtocols : BaseApiResponse
    {
        public SwitchingProtocols(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.SwitchingProtocols, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.SwitchingProtocols), message, data);
        }
    }
}
