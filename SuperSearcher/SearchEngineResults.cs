using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSearcher
{
    /// <summary>
    /// Groups search results and the name of the location that was searched together.
    /// </summary>
    public class SearchEngineResults
    {
        /// <summary>
        /// The name of the location that was searched.
        /// </summary>
        public string SearchLocationName { get; set; } = "";

        /// <summary>
        /// The search results the search engine returned.
        /// </summary>
        public List<ISearchResult> SearchResults { get; set; } = new();
    }
}
