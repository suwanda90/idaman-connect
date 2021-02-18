using Newtonsoft.Json;

namespace IdAManConnect.ViewModels.Idaman
{
    public class TokenIdamanViewModel
    {
        public string ClientId { get; set; }

        public string Scopes { get; set; }

        public string GrantType { get; set; }

        public string ClientSecret { get; set; }
    }
}