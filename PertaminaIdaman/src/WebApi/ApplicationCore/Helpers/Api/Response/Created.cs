using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class Created : BaseApiResponse
    {
        public Created(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.Created, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.Created), message, data);
        }
    }
}
