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
        /// The name of the location the search engine searches.
        /// </summary>
        string SearchLocationName { get; }

        /// <summary>
        /// Searches for some text.
        /// </summary>
        /// <param name="searchText">The text to search for.</param>
        /// <param name="maxResults">The maximum number of results to return.</param>
        /// <returns>A SearchEngineResults object containing the name of the search engine and a list of ISearchResult.</returns>
        Task<SearchEngineResults> Search(string searchText, int maxResults);
    }
}
