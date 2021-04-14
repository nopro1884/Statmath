using Newtonsoft.Json;

namespace Statmath.Application.Models
{
    public class PlanViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "machine")]
        public string Machine { get; set; }

        [JsonProperty(PropertyName = "job")]
        public int Job { get; set; }

        [JsonProperty(PropertyName = "start")]
        public string Start { get; set; }

        [JsonProperty(PropertyName = "end")]
        public string End { get; set; }
    }
}