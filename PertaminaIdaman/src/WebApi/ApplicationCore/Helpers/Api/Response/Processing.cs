using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class Processing : BaseApiResponse
    {
        public Processing(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.Processing, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.Processing), message, data);
        }
    }
}
