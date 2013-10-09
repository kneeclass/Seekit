using System;
using System.Web;

namespace Seekit.Facets {
    internal class FacetsCacheManager<T>
    {
        private const string CacheKeyFormat = "facet-{0}-{1}-{2}";

        internal FacetContext<T> Get(string crawlStamp, string lang, string model) {
            return HttpRuntime.Cache.Get(string.Format(CacheKeyFormat, crawlStamp,lang ?? "", model ?? "")) as FacetContext<T>;
        }

        internal void Add(FacetContext<T> facetContext, string crawlStamp, string lang, string model) {
            HttpRuntime.Cache.Insert(string.Format(CacheKeyFormat, crawlStamp,lang ?? "", model ?? ""), facetContext, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(30));

        }
    }
}
