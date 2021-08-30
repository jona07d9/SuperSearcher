using SuperSearcher;
using System.Collections.Generic;

namespace SuperSearcherWPF.ViewModels
{
    /// <summary>
    /// View model for SearchEngineResults.
    /// </summary>
    public class SearchEngineResultsViewModel : ViewModel
    {
        /// <summary>
        /// The search results returned by the search engine.
        /// </summary>
        private readonly List<SearchResultViewModel> _results;

        /// <summary>
        /// Identifies which search engine the results come from.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// The search results returned by the search engine.
        /// </summary>
        public IReadOnlyList<SearchResultViewModel> Results
        {
            get => _results.AsReadOnly();
        }

        /// <summary>
        /// Converts the search results into view models.
        /// </summary>
        /// <param name="identifier">Used to identify which search engine the results came from.</param>
        /// <param name="results">The search results returned by the search engine.</param>
        public SearchEngineResultsViewModel(
            ViewModelContext context, string identifier, List<ISearchResult> results)
            : base(context)
        {
            _results = results.ConvertAll(searchResult =>
            {
                return new SearchResultViewModel(_context, searchResult);
            });
            Identifier = identifier;
        }
    }
}
