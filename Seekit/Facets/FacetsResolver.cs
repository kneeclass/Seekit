using System;
using Newtonsoft.Json;
using Seekit.Connection;
using Seekit.Extensions;
using Seekit.Settings;

namespace Seekit.Facets {
    internal class FacetsResolver<T>
    {
        private readonly SeekitConfiguration _configuration;

        public FacetsResolver(SeekitConfiguration configuration)
        {
            _configuration = configuration;
        }

        public FacetContext<T> Get(string crawlStamp,string lang, Type model)
        {
            string modelName = model == null ? null : model.JsonNetFormat();

            return GetFacetsJsonDataFromCache(crawlStamp,lang, modelName) ??
                   FetchFacetsJsonDataFromServer(crawlStamp,lang, modelName);

        }

        private static FacetContext<T> GetFacetsJsonDataFromCache(string crawlStamp,string lang, string model) {
            
            var cacheManager = new FacetsCacheManager<T>();
            var facetContext = cacheManager.Get(crawlStamp,lang, model);
            return facetContext;
        }

        private FacetContext<T> FetchFacetsJsonDataFromServer(string crawlStamp, string lang, string model) {
            var facetOperation = new FacetOperation();
            var jsonData = facetOperation.FetchAllFacets(_configuration,lang, model);
            var retval = JsonConvert.DeserializeObject<FacetContext<T>>(jsonData);

            var cacheManager = new FacetsCacheManager<T>();
            cacheManager.Add(retval, crawlStamp,lang, model);

            return retval;
        }

    }
}
