using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class Found : BaseApiResponse
    {
        public Found(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.Found, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.Found), message, data);
        }
    }
}
