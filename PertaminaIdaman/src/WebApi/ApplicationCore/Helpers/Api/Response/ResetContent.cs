using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class ResetContent : BaseApiResponse
    {
        public ResetContent(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.ResetContent, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.ResetContent), message, data);
        }
    }
}
