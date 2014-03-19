using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Seekit.Linq;

namespace Seekit.Entities {
    public class Query {
        public string SearchTerms { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Lang { get; set; }

        public Int32 Take { get; set; }

        public Int32 Skip { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ModelType { get; set; }

        public Int32 QueryId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public GeoQuery GeoQuery { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<IQueryPart> Filter { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<SortOrder> SortOrders { get; set; }
    }

}
