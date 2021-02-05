using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class NoContent : BaseApiResponse
    {
        public NoContent(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.NoContent, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.NoContent), message, data);
        }
    }
}
