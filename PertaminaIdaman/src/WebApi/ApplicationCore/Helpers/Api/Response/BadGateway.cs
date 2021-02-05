using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class BadGateway : BaseApiResponse
    {
        public BadGateway(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.BadGateway, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.BadGateway), message, data);
        }
    }
}
