using System.Collections.Generic;
using Seekit.Linq;

namespace Seekit.Models {
    public class QueryModel {
        public string Query { get; set; }
        public string ModelType { get; set; }
        public string Client { get; set; }
        public List<ConvertedExpression> Filter { get; set; }
    }
}
