using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class RequestEntityTooLarge : BaseApiResponse
    {
        public RequestEntityTooLarge(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.RequestEntityTooLarge, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.RequestEntityTooLarge), message, data);
        }
    }
}
