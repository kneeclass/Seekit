using System.Runtime.Serialization;

namespace Seekit {
    public class SearchModelBase {
        /// <summary>
        /// The absolute url of the page/file
        /// </summary>
        [IgnoreDataMember]
        public string Url { get; set; }
        /// <summary>
        /// The Language ID
        /// </summary>
        [IgnoreDataMember]
        public string Language { get; set; }
    }
}
