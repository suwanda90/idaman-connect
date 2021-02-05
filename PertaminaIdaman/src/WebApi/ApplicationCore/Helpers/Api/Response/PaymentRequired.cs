using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class PaymentRequired : BaseApiResponse
    {
        public PaymentRequired(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.PaymentRequired, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.PaymentRequired), message, data);
        }
    }
}
