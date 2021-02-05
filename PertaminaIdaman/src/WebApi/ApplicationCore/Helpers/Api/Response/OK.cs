using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class OK : BaseApiResponse
    {
        public OK(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.OK, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.OK), message, data);
        }
    }
}
