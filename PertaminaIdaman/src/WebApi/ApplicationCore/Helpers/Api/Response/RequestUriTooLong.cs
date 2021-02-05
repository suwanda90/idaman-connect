using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class RequestUriTooLong : BaseApiResponse
    {
        public RequestUriTooLong(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.RequestUriTooLong, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.RequestUriTooLong), message, data);
        }
    }
}
