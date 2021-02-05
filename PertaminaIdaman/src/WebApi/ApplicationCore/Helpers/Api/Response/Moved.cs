using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class Moved : BaseApiResponse
    {
        public Moved(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.Moved, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.Moved), message, data);
        }
    }
}
