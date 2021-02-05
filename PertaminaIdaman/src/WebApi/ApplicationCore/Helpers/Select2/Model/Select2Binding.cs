using System;
using System.Text.Json.Serialization;

namespace ApplicationCore.Helpers.Select2.Model
{
    public class Select2Binding
    {
        [JsonPropertyName("id")]
        public object Id { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
