using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class TooManyRequests : BaseApiResponse
    {
        public TooManyRequests(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.TooManyRequests, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.TooManyRequests), message, data);
        }
    }
}
