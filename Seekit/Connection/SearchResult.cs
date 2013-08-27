using System.Collections.Generic;

namespace Seekit.Connection {
    public class SearchResult<T> {
        public string CrawlStamp { get; set; }        
        public List<Facet> Facets { get; set; }
        public string Query { get; set; }
        public List<SearchHit<T>> Hits { get; set; }

    }
}
