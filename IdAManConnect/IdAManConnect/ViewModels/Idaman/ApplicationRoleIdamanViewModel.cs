﻿using Newtonsoft.Json;

namespace IdAManConnect.ViewModels.Idaman
{
    public class ApplicationRoleIdamanViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}