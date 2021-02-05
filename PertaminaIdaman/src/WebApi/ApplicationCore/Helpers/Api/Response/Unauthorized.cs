using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class Unauthorized : BaseApiResponse
    {
        public Unauthorized(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.Unauthorized, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.Unauthorized), message, data);
        }
    }
}
