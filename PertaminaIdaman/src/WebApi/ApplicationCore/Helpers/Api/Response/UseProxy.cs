using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class UseProxy : BaseApiResponse
    {
        public UseProxy(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.UseProxy, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.UseProxy), message, data);
        }
    }
}
