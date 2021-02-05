using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class PreconditionRequired : BaseApiResponse
    {
        public PreconditionRequired(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.PreconditionRequired, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.PreconditionRequired), message, data);
        }
    }
}
