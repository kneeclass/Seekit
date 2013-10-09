using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Seekit.Facets {
    public class Facet {
        [JsonProperty(PropertyName = "Key")]
        public string PropetyName { get; set; }
        [JsonProperty(PropertyName = "Value")]
        public List<FacetValue> FacetValues { get; set; }

        public Facet Clone()
        {
            var clone = new Facet();
            clone.PropetyName = PropetyName;
            clone.FacetValues = new List<FacetValue>();
            foreach (var facetValue in FacetValues) {
                clone.FacetValues.Add(new FacetValue
                                          {
                                              Hits = facetValue.Hits,
                                              Name = facetValue.Name,
                                              TotalNumbersOfHits = facetValue.TotalNumbersOfHits
                                          });
            }
            return clone;

        }

    }

}

