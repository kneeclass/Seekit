using System.Collections.Generic;

namespace Seekit.Models {
    public class QueryContext {
        public QueryContext()
        {
            Querys = new List<Query>();
        }
        public string Client { get; set; }
        public List<Query> Querys { get; set; }
    }
}
