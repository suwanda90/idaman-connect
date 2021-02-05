using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class InsufficientStorage : BaseApiResponse
    {
        public InsufficientStorage(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.InsufficientStorage, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.InsufficientStorage), message, data);
        }
    }
}
