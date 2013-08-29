namespace Seekit.Models {
    public class SearchHit<T> {
        public string Url { get; set; }
        public string ContentHightlight { get; set; }
        public string HeadingHightlight { get; set; }
        public T SearchModel { get; set; }
    }
}
