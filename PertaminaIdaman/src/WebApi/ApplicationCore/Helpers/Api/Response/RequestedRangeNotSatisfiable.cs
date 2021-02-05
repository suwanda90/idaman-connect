using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class RequestedRangeNotSatisfiable : BaseApiResponse
    {
        public RequestedRangeNotSatisfiable(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.RequestedRangeNotSatisfiable, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.RequestedRangeNotSatisfiable), message, data);
        }
    }
}
