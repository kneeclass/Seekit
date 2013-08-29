namespace Seekit.Facets {
    public class FacetContext<T> {
        public string CrawlStamp { get; set; }
        public FacetsList<T> Facets { get; set; }
    }
}
