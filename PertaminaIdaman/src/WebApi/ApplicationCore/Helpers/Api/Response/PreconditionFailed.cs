using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class PreconditionFailed : BaseApiResponse
    {
        public PreconditionFailed(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.PreconditionFailed, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.PreconditionFailed), message, data);
        }
    }
}
