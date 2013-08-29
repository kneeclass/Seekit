using System;
using Newtonsoft.Json;
using Seekit.Connection;
using Seekit.Settings;

namespace Seekit.Facets {
    internal class FacetsResolver<T>
    {
        private readonly SeekitConfiguration _configuration;

        public FacetsResolver(SeekitConfiguration configuration)
        {
            _configuration = configuration;
        }

        public FacetContext<T> Get(string crawlStamp, Type model)
        {
            string modelName = model == null ? null : model.FullName;

            return GetFacetsJsonDataFromCache(crawlStamp, modelName) ??
                   FetchFacetsJsonDataFromServer(crawlStamp, modelName);

        }

        private static FacetContext<T> GetFacetsJsonDataFromCache(string crawlStamp, string model) {
            
            var cacheManager = new FacetsCacheManager<T>();
            var facetContext = cacheManager.Get(crawlStamp, model);
            return facetContext;
        }

        private FacetContext<T> FetchFacetsJsonDataFromServer(string crawlStamp, string model) {
            var facetOperation = new FacetOperation();
            var jsonData = facetOperation.FetchAllFacets(_configuration, model);
            var retval = JsonConvert.DeserializeObject<FacetContext<T>>(jsonData);

            var cacheManager = new FacetsCacheManager<T>();
            cacheManager.Add(retval, crawlStamp, model);

            return retval;
        }

    }
}
