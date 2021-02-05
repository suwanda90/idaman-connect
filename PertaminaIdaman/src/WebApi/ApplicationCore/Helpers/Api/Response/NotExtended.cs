using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class NotExtended : BaseApiResponse
    {
        public NotExtended(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.NotExtended, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.NotExtended), message, data);
        }
    }
}
