using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seekit.Connection {
    public class SearchResult<T> {
        
        public List<Facet> Facets { get; set; }
        public string Query { get; set; }
        public List<SearchHit<T>> Hits { get; set; }

    }
}
