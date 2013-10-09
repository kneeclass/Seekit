using System.Collections.Generic;
using Seekit.Models;

namespace Seekit
{
    public interface IMultiSearch
    {
        List<SearchClientBase> SearchClients { get; set; }
        SearchResultContext<SearchModelBase> Search();
    }
}