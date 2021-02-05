using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class Accepted : BaseApiResponse
    {
        public Accepted(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.Accepted, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.Accepted), message, data);
        }
    }
}
