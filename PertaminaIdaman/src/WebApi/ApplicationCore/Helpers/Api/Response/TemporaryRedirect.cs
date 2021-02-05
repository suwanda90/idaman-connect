using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class TemporaryRedirect : BaseApiResponse
    {
        public TemporaryRedirect(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.TemporaryRedirect, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.TemporaryRedirect), message, data);
        }
    }
}
