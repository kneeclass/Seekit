namespace Seekit.Entities {
    public class SearchHit<T> : SearchHit {
        public T SearchModel { get; set; }
        internal SearchHit<TR> ConvertType<TR>() where TR : SearchModelBase {
            return new SearchHit<TR> {
                                  ContentHightlight = ContentHightlight,
                                  HeadingHightlight = HeadingHightlight,
                                  Url = Url,
                                  SearchModel = SearchModel as TR
                              };
            
        }

    }
    public class SearchHit
    {
        public string Url { get; set; }
        public string ContentHightlight { get; set; }
        public string HeadingHightlight { get; set; }
    }
}
