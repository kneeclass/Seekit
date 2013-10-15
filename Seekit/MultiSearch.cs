using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Seekit.Connection;
using Seekit.Entities;
using Seekit.Facets;
using Seekit.Settings;

namespace Seekit {
    public class MultiSearch : ClientBase
    {

        public List<ISearchClient> SearchClients { get; set; }
        private Dictionary<Int32, ISearchClient> QueryIdToClient { get; set;}
        public MultiSearch()
        {
            SearchClients = new List<ISearchClient>();
            QueryIdToClient = new Dictionary<int, ISearchClient>();
            Configuration = SeekitConfiguration.GetConfiguration();
        }
        public MultiSearch(IEnumerable<ISearchClient> searchClients) : this() {

            SearchClients.AddRange(searchClients);
        }
        
        public SearchResultContext<SearchModelBase> Search() {
            var requester = new SearchOperation();
            var queryContext = new QueryContext{Client = Configuration.ClientGuid.ToString()};
            
            for(var a = 0; a < SearchClients.Count; a++) {
                SearchClients[a].Query.QueryId = a+1;
                QueryIdToClient.Add(SearchClients[a].Query.QueryId, SearchClients[a]);
                queryContext.Querys.Add(SearchClients[a].Query);
            }


            var jsonData = requester.PreformSearch(JsonConvert.SerializeObject(queryContext, Formatting.None), Configuration);
            var result = JsonConvert.DeserializeObject<SearchResultContext<SearchModelBase>>(jsonData,new JsonSerializerSettings{TypeNameHandling = TypeNameHandling.All});

            foreach (var searchResult in result.SearchResults) {
                var client = QueryIdToClient[searchResult.QueryId];
                var fcm = new FacetContextMerger<object>();
                var genericType = client.GetType().GetGenericArguments()[0];

                fcm.MergeFacets(result.CrawlStamp, searchResult.Facets, client.IncEmptyFacets, client.Lang, genericType);
            }
            return result;
        }

    }
}
