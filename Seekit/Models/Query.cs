using System;
using System.Collections.Generic;
using Seekit.Linq;

namespace Seekit.Models {
    public class Query {
        public string SearchTerms { get; set; }
        public string Lang { get; set; }
        public Int32 Take { get; set; }
        public Int32 Skip { get; set; }
        public string ModelType { get; set; }
        public Int32 QueryId { get; set; }
        public GeoQuery GeoQuery { get; set; }
        public List<ConvertedExpression> Filter { get; set; }
    }

}
