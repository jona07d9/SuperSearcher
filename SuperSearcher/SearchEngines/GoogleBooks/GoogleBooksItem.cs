using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SuperSearcher.SearchEngines.GoogleBooks
{
    /// <summary>
    /// An item from the Google Books API JSON response.
    /// </summary>
    internal class GoogleBooksItem
    {
        /// <summary>
        /// Information about this item.
        /// </summary>
        [JsonPropertyName("volumeInfo")]
        public GoogleBooksVolumeInfo VolumeInfo { get; set; } = new GoogleBooksVolumeInfo();
    }
}
