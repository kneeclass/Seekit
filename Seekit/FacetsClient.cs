using System;
using Newtonsoft.Json;
using Seekit.Connection;
using Seekit.Facets;
using Seekit.Models;
using Seekit.Settings;

namespace Seekit {
    public class FacetsClient : ClientBase {

        public FacetsClient() {
            Configuration = SeekitConfiguration.GetConfiguration();
        }

        public FacetContext<object> GetAllFacets(string crawlStamp = "")
        {
            var resolver = new FacetsResolver<object>(Configuration);
            return resolver.Get(crawlStamp, null);
        }

        public FacetContext<T> GetAllFacets<T>(string crawlStamp = "") {
            var resolver = new FacetsResolver<T>(Configuration);
            return resolver.Get(crawlStamp, typeof(T));
        }


        
        


    }
}
