using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class SeeOther : BaseApiResponse
    {
        public SeeOther(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.SeeOther, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.SeeOther), message, data);
        }
    }
}
