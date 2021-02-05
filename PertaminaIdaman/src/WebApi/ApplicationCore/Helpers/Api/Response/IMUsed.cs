using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class IMUsed : BaseApiResponse
    {
        public IMUsed(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.IMUsed, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.IMUsed), message, data);
        }
    }
}
