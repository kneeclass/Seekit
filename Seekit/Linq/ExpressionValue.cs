using System;
using System.Collections.Generic;

namespace Seekit.Linq {
    public class ExpressionValue {
        public ExpressionValue()
        {
            Values = new List<object>();
        }
        internal List<object> Values { get; set; }

        public object JsonReturnValue {get; set; }

        public void SetValue(object value) {
            Values.Add(value);
        }
        public override string ToString() {
            return string.Concat(Values);
        }

    }
}
