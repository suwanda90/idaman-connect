using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class LengthRequired : BaseApiResponse
    {
        public LengthRequired(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.LengthRequired, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.LengthRequired), message, data);
        }
    }
}
