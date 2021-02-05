using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class Conflict : BaseApiResponse
    {
        public Conflict(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.Conflict, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.Conflict), message, data);
        }
    }
}
