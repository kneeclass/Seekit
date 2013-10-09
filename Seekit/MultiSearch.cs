using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Seekit.Connection;
using Seekit.Models;
using Seekit.Settings;

namespace Seekit {
    public class MultiSearch : ClientBase
    {

        public List<SearchClientBase> SearchClients { get; set; }
        private Dictionary<Int32, SearchClientBase> QueryIdToClient { get; set;}
        public MultiSearch()
        {
            SearchClients = new List<SearchClientBase>();
            QueryIdToClient = new Dictionary<int, SearchClientBase>();
            Configuration = SeekitConfiguration.GetConfiguration();
        }
        public MultiSearch(IEnumerable<SearchClientBase> searchClients) : this() {
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
                client.MergeFacets(result.CrawlStamp, searchResult.Facets, client.ModelType);
            }
            return result;
        }

    }
}
