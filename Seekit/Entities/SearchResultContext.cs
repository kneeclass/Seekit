using System.Collections.Generic;

namespace Seekit.Entities {
    public class SearchResultContext<T> : SearchResultContextBase {
        public List<SearchResult<T>> SearchResults { get; set; }
    }


    public abstract class SearchResultContextBase
    {
        public string CrawlStamp { get; set; }
    }


}
