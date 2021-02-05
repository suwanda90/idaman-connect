using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class MultiStatus : BaseApiResponse
    {
        public MultiStatus(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.MultiStatus, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.MultiStatus), message, data);
        }
    }
}
