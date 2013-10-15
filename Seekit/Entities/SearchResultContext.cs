using System.Collections.Generic;

namespace Seekit.Entities {
    public class SearchResultContext<T> {
        public string CrawlStamp { get; set; }
        public List<SearchResult<T>> SearchResults { get; set; }
    }

}
