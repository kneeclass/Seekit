using System;
using Newtonsoft.Json;

namespace Seekit.Linq {
    //class ExpressionValueJsonConverter : JsonConverter {
        //public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        //{


        //    if (value == null)
        //    {
        //        writer.WriteNull();
        //    }
        //    else
        //    {
        //        var returnType = value.GetType();
        //        if (returnType == typeof (string) || returnType.IsValueType) {
        //            writer.WriteValue(value);
        //        }
        //        else {
        //            serializer.Serialize(writer, value);
        //        }
        //    }


        //}

        //public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool CanConvert(Type objectType)
        //{
        //    return objectType == typeof (ExpressionValue);
        //}
    //}
}
