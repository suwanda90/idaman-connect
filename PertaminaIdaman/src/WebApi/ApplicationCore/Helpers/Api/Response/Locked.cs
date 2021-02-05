using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class Locked : BaseApiResponse
    {
        public Locked(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.Locked, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.Locked), message, data);
        }
    }
}
