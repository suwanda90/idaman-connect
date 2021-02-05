using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApplicationCore.Helpers.Select2.Model
{
    public class Select2Result
    {
        [JsonPropertyName("result")]
        public IList<Select2Binding> Result { get; set; }

        [JsonPropertyName("key")]
        public object Key { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
