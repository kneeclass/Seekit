using System;
using System.Web;

namespace Seekit.Facets {
    internal class FacetsCacheManager<T>
    {
        private const string CacheKeyFormat = "facet-{0}-{1}";

        internal FacetContext<T> Get(string crawlStamp, string model) {
            return HttpRuntime.Cache.Get(string.Format(CacheKeyFormat, crawlStamp, model ?? "")) as FacetContext<T>;
        }

        internal void Add(FacetContext<T> facetContext, string crawlStamp, string model) {
            HttpRuntime.Cache.Insert(string.Format(CacheKeyFormat, crawlStamp, model ?? ""), facetContext, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(30));

        }
    }
}
