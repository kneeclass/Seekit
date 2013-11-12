using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Text;
using Seekit.Extensions;
using Seekit.Settings;
using Newtonsoft.Json;
using System.Web;
using System.Collections.Generic;

namespace Seekit.Web.UI {
    internal class MarkupGenerator {

        public string GenerateMetaTags(SearchModelBase searchModel) {
            var sb = new StringBuilder();
            var searchModelType = searchModel.GetType();
            var properties = searchModelType.GetProperties();

            foreach (var propertyInfo in properties) {

                if(Ignore(propertyInfo)) continue;

                var value = GetValue(propertyInfo, searchModel);
                if (value == null)
                    value = "null";

                var name = GetName(propertyInfo);

                if(name.ToLower() == Constants.SeekitTypedModelsName.ToLower())
                    throw new DuplicateNameException("The propetyname: " +Constants.SeekitTypedModelsName +" is used internaly by seekit and may not be used as a SearchModel property name");

                var metaTag = string.Format(Constants.SeekitMetaTagFormat, 
                    name,
                    value,
                    GetType(propertyInfo),
                    IsFacet(propertyInfo));

                sb.AppendLine(metaTag);
            }

            sb.AppendLine(SearchModelTypes(searchModel));

            return sb.ToString();
        }

        private string SearchModelTypes(SearchModelBase searchModel) {
            var currentType = searchModel.GetType();
            var typesList = new List<string>();
            while (currentType != null && currentType.FullName != "System.Object") {
                typesList.Add(currentType.JsonNetFormat());
                currentType = currentType.BaseType;
            }
            return string.Format(Constants.SeekitMetaTagFormat,
                        Constants.SeekitTypedModelsName,
                        HttpUtility.HtmlEncode(JsonConvert.SerializeObject(typesList)),
                        "IEnumerable",
                        false);

        }

        private static bool Ignore(PropertyInfo propertyInfo) {
            return propertyInfo.GetCustomAttributes(true).Any(x => x is IgnoreDataMemberAttribute);
        }

        private static string GetName(PropertyInfo propertyInfo) {
            return propertyInfo.Name;
        }
        private static string GetValue(PropertyInfo propertyInfo, SearchModelBase searchModel) {
            var value = propertyInfo.GetValue(searchModel, null);

            if(value == null)
                return null;

            if(value is DateTime) {
                return ((DateTime) value).ToString(Constants.SeekitDefaultDateTimeFormat);
            }

            if(value is string) {
                return HttpUtility.HtmlEncode(value);
            }
            if (value is Enum)
            {
                return value.ToString();
            }

            if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType)){
                return HttpUtility.HtmlEncode(JsonConvert.SerializeObject(value));
            }

            return HttpUtility.HtmlEncode(JsonConvert.SerializeObject(value));
        }
        private static string GetType(PropertyInfo propertyInfo) {

            if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType) && propertyInfo.PropertyType != typeof(string)) {
                return typeof(IEnumerable).Name;
            }

            if (propertyInfo.PropertyType.IsEnum) {
                return typeof (string).Name;
            }

            if (propertyInfo.PropertyType.IsGenericType &&
                propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof (Nullable<>)) {
                    if (!propertyInfo.PropertyType.GetGenericArguments().Any())
                        throw new NullReferenceException("Missing nullable type");
                    var genericType = propertyInfo.PropertyType.GetGenericArguments()[0];
                    return genericType.IsEnum
                        ? typeof (string).Name
                        : genericType.Name;
                }


            return propertyInfo.PropertyType.Name;
        }
        private static string IsFacet(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttributes(true).Any(x => x is FacetAttribute).ToString();
        }
        

    }
}
