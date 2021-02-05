using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class UnprocessableEntity : BaseApiResponse
    {
        public UnprocessableEntity(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.UnprocessableEntity, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.UnprocessableEntity), message, data);
        }
    }
}
