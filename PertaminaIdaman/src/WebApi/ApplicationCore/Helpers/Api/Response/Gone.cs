using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class Gone : BaseApiResponse
    {
        public Gone(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.Gone, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.Gone), message, data);
        }
    }
}
