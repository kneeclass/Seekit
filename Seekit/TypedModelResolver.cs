using System.Web;
using Seekit.Extensions;
using Seekit.Web.UI;

namespace Seekit {
    public class TypedModelResolver {
        /// <summary>
        /// Outputs metatags that the seekit crawler will recognize and use
        /// </summary>
        /// <param name="searchModel">The search model to be output as metatags</param>
        /// <param name="onlyShowIfCrawlerOrDebug">Only output the metatags if the request is from the crawler or the application is in debug mode.</param>
        /// <returns></returns>
        public static string OutputMetaFor(SearchModelBase searchModel, bool onlyShowIfCrawlerOrDebug = true) {

            if (searchModel == null)
                return "<!-- Searchmodel was null -->";

            if (onlyShowIfCrawlerOrDebug && !HttpContext.Current.Request.IsSeekitCrawler() && !HttpContext.Current.IsDebuggingEnabled) {
                return string.Empty;
            }

            var generator = new MarkupGenerator();
            return generator.GenerateMetaTags(searchModel);
        }


        /// <summary>
        /// Outputs a HTML page that includes the metatags from the SearchModel
        /// </summary>
        /// <param name="searchModel">The search model to be output as metatags</param>
        /// <param name="lang">Override the lang attribute from the crawler</param>
        /// <returns></returns>
        public static string OutputHtmlForFile(SearchModelBase searchModel,string lang = "")
        {
            return string.Format(@"<!DOCTYPE HTML>
                                    <html lang="""+lang+@""">
                                        <head>
                                            "+OutputMetaFor(searchModel,false)+@"
                                        </head>
                                        <body>
                                        </body>
                                    </html>");
        }
    }
}
    