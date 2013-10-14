﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Seekit.Models {
    public class SortOrder {
        public string PropertyName { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Order Order { get; set; }
    }
    public enum Order
    {
        Ascending,
        Descending
    }


}
