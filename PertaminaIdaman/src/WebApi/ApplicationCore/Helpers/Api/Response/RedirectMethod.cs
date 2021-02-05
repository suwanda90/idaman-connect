using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class RedirectMethod : BaseApiResponse
    {
        public RedirectMethod(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.RedirectMethod, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.RedirectMethod), message, data);
        }
    }
}
