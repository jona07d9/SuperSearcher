using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SuperSearcher.SearchEngines.GoogleBooks
{
    /// <summary>
    /// Information about an item.
    /// </summary>
    internal class GoogleBooksVolumeInfo
    {
        /// <summary>
        /// The item's title.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = "";

        /// <summary>
        /// A link to see more information about the item.
        /// </summary>
        [JsonPropertyName("infoLink")]
        public string InfoLink { get; set; } = "";
    }
}
