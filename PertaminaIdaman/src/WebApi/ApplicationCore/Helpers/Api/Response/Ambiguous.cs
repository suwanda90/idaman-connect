using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class Ambiguous : BaseApiResponse
    {
        public Ambiguous(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.Ambiguous, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.Ambiguous), message, data);
        }
    }
}
