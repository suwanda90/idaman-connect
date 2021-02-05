using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class VariantAlsoNegotiates : BaseApiResponse
    {
        public VariantAlsoNegotiates(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.VariantAlsoNegotiates, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.VariantAlsoNegotiates), message, data);
        }
    }
}
