using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class MultipleChoices : BaseApiResponse
    {
        public MultipleChoices(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.MultipleChoices, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.MultipleChoices), message, data);
        }
    }
}
