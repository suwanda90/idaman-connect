using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class AlreadyReported : BaseApiResponse
    {
        public AlreadyReported(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.AlreadyReported, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.AlreadyReported), message, data);
        }
    }
}
