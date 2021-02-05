using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class ServiceUnavailable : BaseApiResponse
    {
        public ServiceUnavailable(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.ServiceUnavailable, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.ServiceUnavailable), message, data);
        }
    }
}
