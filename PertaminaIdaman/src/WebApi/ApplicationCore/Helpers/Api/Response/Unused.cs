using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class Unused : BaseApiResponse
    {
        public Unused(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.Unused, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.Unused), message, data);
        }
    }
}
