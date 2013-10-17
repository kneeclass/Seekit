using System.Web;
using Seekit.Extensions;
using Seekit.Web.UI;

namespace Seekit {
    public class TypedModelResolver {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel">The search model to be output as metatags</param>
        /// <param name="onlyShowIfCrawlerOrDebug">Only output the metatags if the request is from the crawler or the application is in debug mode.</param>
        /// <returns></returns>
        public static string OutputMetaFor(SearchModelBase searchModel, bool onlyShowIfCrawlerOrDebug = true)
        {

            if (searchModel == null)
                return "<!-- Searchmodel was null -->";

            if(onlyShowIfCrawlerOrDebug && !HttpContext.Current.Request.IsSeekitCrawler() && !HttpContext.Current.IsDebuggingEnabled) {
                return string.Empty;
            }

            var generator = new MarkupGenerator();
            return generator.GenerateMetaTags(searchModel);
        }

    }
}