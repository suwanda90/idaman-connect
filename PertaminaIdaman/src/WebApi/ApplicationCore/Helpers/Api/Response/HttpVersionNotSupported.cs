using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class HttpVersionNotSupported : BaseApiResponse
    {
        public HttpVersionNotSupported(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.HttpVersionNotSupported, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.HttpVersionNotSupported), message, data);
        }
    }
}
