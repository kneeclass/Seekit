using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Seekit.Settings;

namespace Seekit.Extensions {
    public static class HttpContextExtensions {

        public static bool IsSeekitCrawler(this HttpRequest httpRequest) {
            return httpRequest.UserAgent == Constants.SeekitCrawlerUserAgent;
        }
    }
}
