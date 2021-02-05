using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class RequestHeaderFieldsTooLarge : BaseApiResponse
    {
        public RequestHeaderFieldsTooLarge(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.RequestHeaderFieldsTooLarge, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.RequestHeaderFieldsTooLarge), message, data);
        }
    }
}
