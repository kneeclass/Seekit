using System;
using Seekit.Facets;
using Seekit.Models;
using Seekit.Settings;

namespace Seekit {
    public class FacetsClient : ClientBase {

        public FacetsClient() {
            Configuration = SeekitConfiguration.GetConfiguration();
        }

        public FacetContext<SearchModelBase> GetAllFacets(string crawlStamp = "", string lang = "")
        {
            var resolver = new FacetsResolver<SearchModelBase>(Configuration);
            return resolver.Get(crawlStamp,lang, null);
        }

        public FacetContext<T> GetAllFacets<T>(string crawlStamp = "", string lang = "", Type typeOverride = null) {
            var resolver = new FacetsResolver<T>(Configuration);
            return resolver.Get(crawlStamp, lang, typeOverride ?? typeof(T));
        }



        
        


    }
}
