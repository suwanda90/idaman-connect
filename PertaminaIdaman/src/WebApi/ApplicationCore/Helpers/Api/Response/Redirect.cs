using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class Redirect : BaseApiResponse
    {
        public Redirect(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.Redirect, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.Redirect), message, data);
        }
    }
}
