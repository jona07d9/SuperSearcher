using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SuperSearcher.SearchEngines.GoogleBooks
{
    /// <summary>
    /// Represents a JSON response from the Google Books API.
    /// </summary>
    internal class GoogleBooksResults
    {
        /// <summary>
        /// The items the request found.
        /// </summary>

        [JsonPropertyName("items")]
        public List<GoogleBooksItem> Items { get; set; } = new List<GoogleBooksItem>();
    }
}
