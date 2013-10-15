using System;
using System.Collections.Generic;
using Seekit.Facets;

namespace Seekit.Entities {
    public class SearchResult<T>{
        public FacetsList<T> Facets { get; set; }
        public string Query { get; set; }
        public List<SearchHit<T>> Hits { get; set; }
        public Int32 TotalNumberOfHits { get; set; }
        public Int32 QueryId { get; set; }
    }
}
