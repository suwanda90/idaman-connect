using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class NotFound : BaseApiResponse
    {
        public NotFound(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.NotFound, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.NotFound), message, data);
        }
    }
}
