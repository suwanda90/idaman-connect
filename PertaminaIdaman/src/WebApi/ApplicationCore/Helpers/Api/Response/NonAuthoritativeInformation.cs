using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class NonAuthoritativeInformation : BaseApiResponse
    {
        public NonAuthoritativeInformation(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.NonAuthoritativeInformation, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.NonAuthoritativeInformation), message, data);
        }
    }
}
