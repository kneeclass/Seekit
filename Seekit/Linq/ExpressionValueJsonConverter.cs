using System;
using Newtonsoft.Json;

namespace Seekit.Linq {
    class ExpressionValueJsonConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

            var expressionValue = value as ExpressionValue;
            if (expressionValue == null){
                writer.WriteValue(string.Empty);
                return;
            }

            if (expressionValue.JsonReturnValue == null) {
                writer.WriteNull();
            }
            else {
                var returnType = expressionValue.JsonReturnValue.GetType();
                if (returnType == typeof (string) || returnType.IsValueType)
                {
                    writer.WriteValue(expressionValue.JsonReturnValue);
                }
                else
                {
                    serializer.Serialize(writer, expressionValue.JsonReturnValue);
                }
            }


        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (ExpressionValue);
        }
    }
}
