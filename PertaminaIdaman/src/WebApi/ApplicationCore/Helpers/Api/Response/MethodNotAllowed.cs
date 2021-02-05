using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class MethodNotAllowed : BaseApiResponse
    {
        public MethodNotAllowed(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.MethodNotAllowed, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.MethodNotAllowed), message, data);
        }
    }
}
