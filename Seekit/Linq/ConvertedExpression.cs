using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Seekit.Linq {
    public class ConvertedExpression {

        public string Condition { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public SubQuery? SubQuery { get; set; }
        public string Target { get; set; }
        public string Equality { get; set; }
        public object Value { get; set; }

    }
    public enum SubQuery
    {
        Open,
        Close
    }

}
