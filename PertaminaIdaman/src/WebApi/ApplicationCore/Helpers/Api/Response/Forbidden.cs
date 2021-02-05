using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class Forbidden : BaseApiResponse
    {
        public Forbidden(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.Forbidden, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.Forbidden), message, data);
        }
    }
}
