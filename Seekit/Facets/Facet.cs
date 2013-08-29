using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Seekit.Facets {
    public class Facet : ICloneable {
        [JsonProperty(PropertyName = "Key")]
        public string PropetyName { get; set; }
        [JsonProperty(PropertyName = "Value")]
        public List<FacetValue> FacetValues { get; set; }

        public object Clone() {
            return MemberwiseClone();
        }
    }

}

