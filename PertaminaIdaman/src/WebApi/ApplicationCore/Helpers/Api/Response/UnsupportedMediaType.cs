using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class UnsupportedMediaType : BaseApiResponse
    {
        public UnsupportedMediaType(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.UnsupportedMediaType, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.UnsupportedMediaType), message, data);
        }
    }
}
