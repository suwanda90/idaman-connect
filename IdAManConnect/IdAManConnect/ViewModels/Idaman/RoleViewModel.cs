using Newtonsoft.Json;

namespace IdAManConnect.ViewModels.Idaman
{
    public class RoleViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "roleName")]
        public string RoleName { get; set; }

        [JsonProperty(PropertyName = "application")]
        public ApplicationViewModel Application { get; set; }
    }
}