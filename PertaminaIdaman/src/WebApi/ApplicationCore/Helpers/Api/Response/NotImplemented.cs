using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class NotImplemented : BaseApiResponse
    {
        public NotImplemented(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.NotImplemented, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.NotImplemented), message, data);
        }
    }
}
