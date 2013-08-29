using System.Collections.Generic;
using Seekit.Connection;
using Seekit.Facets;

namespace Seekit.Models {
    public class SearchResult<T> {
        public string CrawlStamp { get; set; }
        public FacetsList<T> Facets { get; set; }
        public string Query { get; set; }
        public List<SearchHit<T>> Hits { get; set; }

    }
}
