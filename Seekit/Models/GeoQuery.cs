using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seekit.Models {
    public class GeoQuery : GeoLocation {
        public string PropertyName { get; set; }
        public double Distance { get; set; }
    }
}
