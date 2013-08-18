using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seekit.Connection {
    public class SearchHit<T> {
        public string Url { get; set; }
        public string ContentHightlight { get; set; }
        public string HeadingHightlight { get; set; }
        public T SearchModel { get; set; }
    }
}
