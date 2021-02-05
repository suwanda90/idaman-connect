using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class RequestTimeout : BaseApiResponse
    {
        public RequestTimeout(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.RequestTimeout, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.RequestTimeout), message, data);
        }
    }
}
