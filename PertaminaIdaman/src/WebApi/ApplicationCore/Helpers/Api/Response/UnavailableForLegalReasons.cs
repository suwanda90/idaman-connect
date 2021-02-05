using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class UnavailableForLegalReasons : BaseApiResponse
    {
        public UnavailableForLegalReasons(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.UnavailableForLegalReasons, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.UnavailableForLegalReasons), message, data);
        }
    }
}
