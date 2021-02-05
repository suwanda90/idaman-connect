using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class PermanentRedirect : BaseApiResponse
    {
        public PermanentRedirect(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.PermanentRedirect, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.PermanentRedirect), message, data);
        }
    }
}
