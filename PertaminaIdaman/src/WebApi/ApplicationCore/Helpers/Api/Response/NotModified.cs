using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class NotModified : BaseApiResponse
    {
        public NotModified(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.NotModified, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.NotModified), message, data);
        }
    }
}
