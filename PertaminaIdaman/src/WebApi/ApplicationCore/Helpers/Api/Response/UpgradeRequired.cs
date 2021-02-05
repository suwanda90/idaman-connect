using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class UpgradeRequired : BaseApiResponse
    {
        public UpgradeRequired(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.UpgradeRequired, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.UpgradeRequired), message, data);
        }
    }
}
