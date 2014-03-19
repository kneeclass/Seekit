using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Seekit.Entities.QueryParts
{
    public class SubQueryPart : IQueryPart
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SubQuery? SubQuery { get; set; }
    }
    public enum SubQuery
    {
        Open,
        Close
    }
}
