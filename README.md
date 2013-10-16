Seekit Client API
======

The typed crawling search engine powered by Linq, RavenDB and coffee

```csharp

var contentSearchClient = new SearchClient<ContentSearchModel>("Bacon ipsum", "en");
contentSearchClient.Where(x=> x.Price < 100 && x.Quality => 8).Skip(1).Take(42)

SearchResult<ContentSearchModel> result = contentSearchClient.Search();

DoStuffToThe(result);


```
