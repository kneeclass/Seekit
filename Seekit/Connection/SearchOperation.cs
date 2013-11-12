using System;
using System.IO;
using System.Net;
using Seekit.Settings;

namespace Seekit.Connection {
    public class SearchOperation {

        public string PreformSearch(string jsonData, SeekitConfiguration configuration)
        {
            StreamWriter requestWriter;
            var retval = string.Empty;  
            var webRequest = WebRequest.Create(new Uri(configuration.ApiUrl, "search")) as HttpWebRequest;
            if (webRequest != null) {
                webRequest.Method = "POST";
                webRequest.Timeout = 20000;
                webRequest.ContentType = "application/json";
                using (requestWriter = new StreamWriter(webRequest.GetRequestStream())) {
                    requestWriter.Write(jsonData);
                }

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
