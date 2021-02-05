using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class BadRequest : BaseApiResponse
    {
        public BadRequest(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.BadRequest, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.BadRequest), message, data);
        }
    }
}
