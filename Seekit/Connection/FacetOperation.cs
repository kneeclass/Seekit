using System;
using System.IO;
using System.Net;
using Seekit.Settings;

namespace Seekit.Connection {
    public class FacetOperation {

        public string FetchAllFacets(SeekitConfiguration configuration, string modelName) {

            var retval = string.Empty;
            var facetsApiPath = string.Format("facets/{0}", configuration.ClientGuid);

            if(!string.IsNullOrEmpty(modelName)) {
                facetsApiPath += "?model=" + modelName;
            }

            var webRequest = WebRequest.Create(new Uri(configuration.ApiUrl, facetsApiPath)) as HttpWebRequest;
            if (webRequest != null) {
                webRequest.Method = "GET";
                webRequest.Timeout = 20000;
                webRequest.ContentType = "application/json";

                var response = (HttpWebResponse)webRequest.GetResponse();
                try {
                    var streamReader = new StreamReader(response.GetResponseStream(), true);
                    try {
                        retval = streamReader.ReadToEnd();
                    } finally {
                        streamReader.Close();
                    }
                } finally {
                    response.Close();
                }

            }
            return retval;

        }
    }
}
