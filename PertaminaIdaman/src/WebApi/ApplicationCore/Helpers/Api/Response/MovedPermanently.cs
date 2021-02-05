using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class MovedPermanently : BaseApiResponse
    {
        public MovedPermanently(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.MovedPermanently, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.MovedPermanently), message, data);
        }
    }
}
