using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Seekit.Connection {
    public class SearchOperations {

        public string PreformSearch(string data)
        {
            StreamWriter requestWriter;
            string retval = string.Empty;
            var webRequest = System.Net.WebRequest.Create("http://127.0.0.1:81/api/search") as HttpWebRequest;
            if (webRequest != null) {
                webRequest.Method = "POST";
                webRequest.ServicePoint.Expect100Continue = false;
                webRequest.Timeout = 20000;

                webRequest.ContentType = "application/json";
                using (requestWriter = new StreamWriter(webRequest.GetRequestStream())) {
                    requestWriter.Write(data);
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
