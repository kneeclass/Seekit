using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seekit.Models {
    public class SearchResultContext<T> {
        public string CrawlStamp { get; set; }
        public List<SearchResult<T>> SearchResults { get; set; }
    }

}
