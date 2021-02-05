using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ApplicationCore.Helpers.Datatables.Model
{
    public class DatatablesResult<T>
    {
        [JsonProperty(PropertyName = "draw")]
        [JsonPropertyName("draw")]
        public int Draw { get; set; }
        [JsonProperty(PropertyName = "recordsTotal")]
        [JsonPropertyName("recordsTotal")]
        public int RecordsTotal { get; set; }
        [JsonProperty(PropertyName = "recordsFiltered")]
        [JsonPropertyName("recordsFiltered")]
        public int RecordsFiltered { get; set; }
        [JsonProperty(PropertyName = "data")]
        [JsonPropertyName("data")]
        public IEnumerable<T> Data { get; set; }
    }

}
