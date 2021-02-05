using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class ExpectationFailed : BaseApiResponse
    {
        public ExpectationFailed(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.ExpectationFailed, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.ExpectationFailed), message, data);
        }
    }
}
