using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace SuperSearcher.SearchEngines.GoogleBooks
{
    /// <summary>
    /// Searches the Google Books API.
    /// </summary>
    public class GoogleBooksAPI : ISearchEngine
    {
        /// <summary>
        /// Google Books API request URI.
        /// </summary>
        private const string RequestUri = "https://www.googleapis.com/books/v1/volumes?q=";

        /// <summary>
        /// Used to make requests to the API.
        /// </summary>
        private readonly HttpClient _httpClient = new();

        public string SearchLocationName => "Google Books";

        /// <summary>
        /// Sets request headers.
        /// </summary>
        public GoogleBooksAPI()
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Super Searcher");
        }

        /// <summary>
        /// Searches the Google Books API.
        /// </summary>
        /// <param name="searchText">The text to search for.</param>
        /// <param name="maxResults">The maximum number of results to return.</param>
        /// <returns>The names of the books that was found.</returns>
        public async Task<List<ISearchResult>> Search(string searchText, int maxResults)
        {
            Stream stream = await _httpClient.GetStreamAsync(RequestUri + searchText);
            GoogleBooksResults results = await JsonSerializer.DeserializeAsync<GoogleBooksResults>(stream);

            List<ISearchResult> books = new();

            foreach (GoogleBooksItem item in results.Items)
            {
                if (books.Count >= maxResults)
                {
                    break;
                }

                books.Add(new GoogleBooksSearchResult() { Name = item.VolumeInfo.Title, Link = item.VolumeInfo.InfoLink });
            }

            return books;
        }
    }
}
