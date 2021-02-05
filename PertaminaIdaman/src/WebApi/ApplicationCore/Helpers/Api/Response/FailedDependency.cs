using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class FailedDependency : BaseApiResponse
    {
        public FailedDependency(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.FailedDependency, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.FailedDependency), message, data);
        }
    }
}
