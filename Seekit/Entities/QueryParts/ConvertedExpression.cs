using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Seekit.Entities {
    public class ConvertedExpression : IQueryPart {

        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //public string Operator { get; set; }

        //[JsonConverter(typeof(StringEnumConverter))]
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //public SubQuery? SubQuery { get; set; }

        public string Target { get; set; }

        public string Equality { get; set; }

        public object Value { get; set; }

        [JsonIgnore]
        public Type TargetType { get; set; }
    }

}
