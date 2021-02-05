using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class Continue : BaseApiResponse
    {
        public Continue(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.Continue, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.Continue), message, data);
        }
    }
}
