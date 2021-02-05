using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class LoopDetected : BaseApiResponse
    {
        public LoopDetected(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.LoopDetected, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.LoopDetected), message, data);
        }
    }
}
