﻿using System;
using System.Net;
using ApplicationCore.Helpers.Api.Model;

namespace ApplicationCore.Helpers.Api.Response
{
    public class NetworkAuthenticationRequired : BaseApiResponse
    {
        public NetworkAuthenticationRequired(string message, object data)
        {
            SetBaseApiResponse((int)HttpStatusCode.NetworkAuthenticationRequired, Enum.GetName(typeof(HttpStatusCode), (int)HttpStatusCode.NetworkAuthenticationRequired), message, data);
        }
    }
}
