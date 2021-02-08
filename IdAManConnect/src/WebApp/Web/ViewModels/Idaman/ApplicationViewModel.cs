using Newtonsoft.Json;

namespace Web.ViewModels.Idaman
{
    public class ApplicationViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "applicationName")]
        public string ApplicationName { get; set; }
    }
}