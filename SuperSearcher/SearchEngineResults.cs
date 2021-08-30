using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSearcher
{
    /// <summary>
    /// Groups search results with an identifier.
    /// </summary>
    public class SearchEngineResults
    {
        /// <summary>
        /// Used to identify which search engine the results came from.
        /// </summary>
        public string Identifier { get; set; } = "";

        /// <summary>
        /// The search results the search engine returned.
        /// </summary>
        public List<ISearchResult> SearchResults { get; set; } = new();
    }
}
