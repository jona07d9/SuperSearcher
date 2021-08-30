using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSearcher
{
    /// <summary>
    /// Interface for search engines.
    /// </summary>
    public interface ISearchEngine
    {
        /// <summary>
        /// An identifier that will be set on the returned SearchEngineResults,
        /// to identify which search engine the results came from.
        /// </summary>
        string ResultsIdentifier { get; set; }

        /// <summary>
        /// Searches for some text.
        /// </summary>
        /// <param name="searchText">The text to search for.</param>
        /// <param name="maxResults">The maximum number of results to return.</param>
        /// <returns>
        /// A SearchEngineResults object containing the name of the search engine
        /// and a list of ISearchResult.
        /// </returns>
        Task<SearchEngineResults> Search(string searchText, int maxResults);
    }
}
