using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Seekit.Facets;

namespace Seekit.Linq {
    public class ConvertedExpression {

        public ConvertedExpression() {
            Value = new ExpressionValue();
        }

        public string Operator { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
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
