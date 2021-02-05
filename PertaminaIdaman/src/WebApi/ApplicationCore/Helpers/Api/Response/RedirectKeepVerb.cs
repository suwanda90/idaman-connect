using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class RedirectKeepVerb : BaseApiResponse
    {
        public RedirectKeepVerb(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.RedirectKeepVerb, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.RedirectKeepVerb), message, data);
        }
    }
}
