using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Seekit.Facets;

namespace Seekit.Linq {
    public class ConvertedExpression {

        public ConvertedExpression() {
            Value = new ExpressionValue();
        }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Operator { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SubQuery? SubQuery { get; set; }

        public string Target { get; set; }

        public string Equality { get; set; }

        [JsonConverter(typeof(ExpressionValueJsonConverter))]
        public ExpressionValue Value { get; set; }

    }
    public enum SubQuery
    {
        Open,
        Close
    }

}
