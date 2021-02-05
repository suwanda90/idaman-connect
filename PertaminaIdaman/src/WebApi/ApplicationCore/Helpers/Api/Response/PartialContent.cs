using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class PartialContent : BaseApiResponse
    {
        public PartialContent(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.PartialContent, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.PartialContent), message, data);
        }
    }
}
