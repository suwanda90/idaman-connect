using Newtonsoft.Json;
using System.Collections.Generic;

namespace Web.ViewModels.Idaman
{
    public class WhitelistViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "user")]
        public UserViewModel User { get; set; }

        [JsonProperty(PropertyName = "role")]
        public List<RoleViewModel> Role { get; set; }

        [JsonProperty(PropertyName = "isDeleted")]
        public bool IsDeleted { get; set; }
    }
}