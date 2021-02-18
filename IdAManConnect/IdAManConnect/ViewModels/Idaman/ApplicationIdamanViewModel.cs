using Newtonsoft.Json;

namespace IdAManConnect.ViewModels.Idaman
{
    public class ApplicationIdamanViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "clientId")]
        public string ClientId { get; set; }

        [JsonProperty(PropertyName = "createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "created")]
        public string Created { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "updated")]
        public string Updated { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "photo")]
        public string Photo { get; set; }

        [JsonProperty(PropertyName = "clientSecret")]
        public string ClientSecret { get; set; }

        [JsonProperty(PropertyName = "logoutUrl")]
        public string LogoutUrl { get; set; }

        [JsonProperty(PropertyName = "homePageUrl")]
        public string HomePageUrl { get; set; }

        [JsonProperty(PropertyName = "termofServiceUrl")]
        public string TermofServiceUrl { get; set; }

        [JsonProperty(PropertyName = "privacyStatementUrl")]
        public string PrivacyStatementUrl { get; set; }

        [JsonProperty(PropertyName = "publisherDomain")]
        public string PublisherDomain { get; set; }

        [JsonProperty(PropertyName = "redirectUrl")]
        public string RedirectUrl { get; set; }

        [JsonProperty(PropertyName = "isPublished")]
        public string IsPublished { get; set; }

        [JsonProperty(PropertyName = "rattings")]
        public string Rattings { get; set; }

        [JsonProperty(PropertyName = "technologies")]
        public string Technologies { get; set; }

        [JsonProperty(PropertyName = "databases")]
        public string Databases { get; set; }

        [JsonProperty(PropertyName = "programs")]
        public string Programs { get; set; }

        [JsonProperty(PropertyName = "requirements")]
        public string Requirements { get; set; }
    }
}