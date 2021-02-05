using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class InternalServerError : BaseApiResponse
    {
        public InternalServerError(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.InternalServerError, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.InternalServerError), message, data);
        }
    }
}
