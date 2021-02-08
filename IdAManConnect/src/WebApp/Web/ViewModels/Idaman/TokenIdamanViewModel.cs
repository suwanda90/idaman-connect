using Newtonsoft.Json;

namespace Web.ViewModels.Idaman
{
    public class TokenIdamanViewModel
    {
        public string ClientId { get; set; }

        public string Scope { get; set; }

        public string GrantType { get; set; }

        public string ClientSecret { get; set; }
    }
}