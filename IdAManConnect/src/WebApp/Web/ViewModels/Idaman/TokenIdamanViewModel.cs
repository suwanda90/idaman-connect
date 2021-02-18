﻿using Newtonsoft.Json;

namespace Web.ViewModels.Idaman
{
    public class TokenIdamanViewModel
    {
        public string ClientId { get; set; }

        public string Scopes { get; set; }

        public string GrantType { get; set; }

        public string ClientSecret { get; set; }

        public string ApiClientId { get; set; }

        public string ApiScopes { get; set; }

        public string ApiGrantType { get; set; }

        public string ApiClientSecret { get; set; }
    }
}