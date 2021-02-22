using Newtonsoft.Json;

namespace Web.ViewModels.Idaman
{
    public class TokenIdamanViewModel
    {
        public string ClientId { get; set; }

        public string Scopes { get; set; }

        public string GrantType { get; set; }

        public string ClientSecret { get; set; }

        public string IdamanConnectApiClientId { get; set; }

        public string IdamanConnectApiScopes { get; set; }

        public string IdamanConnectApiGrantType { get; set; }

        public string IdamanConnectApiClientSecret { get; set; }
    }
}