using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class EarlyHints : BaseApiResponse
    {
        public EarlyHints(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.EarlyHints, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.EarlyHints), message, data);
        }
    }
}
