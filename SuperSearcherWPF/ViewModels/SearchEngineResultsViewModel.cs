using SuperSearcher;
using System.Collections.Generic;

namespace SuperSearcherWPF.ViewModels
{
    /// <summary>
    /// View model that groups search results and the name of the search engine they came from.
    /// </summary>
    public class SearchEngineResultsViewModel : ViewModel
    {
        /// <summary>
        /// The search results returned by the search engine.
        /// </summary>
        private readonly List<SearchResultViewModel> _results;

        /// <summary>
        /// The name of the search engine the search results came from.
        /// </summary>
        public string EngineName { get; set; }
        /// <summary>
        /// The search results returned by the search engine.
        /// </summary>
        public IReadOnlyList<SearchResultViewModel> Results
        {
            get => _results.AsReadOnly();
        }

        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="engineName">The name of the search engine the search results came from.</param>
        /// <param name="results">The search results returned by the search engine.</param>
        public SearchEngineResultsViewModel(ViewModelContext context, string engineName, List<ISearchResult> results)
            : base(context)
        {
            _results = results.ConvertAll(searchResult =>
            {
                return new SearchResultViewModel(_context, searchResult);
            });
            EngineName = engineName;
        }
    }
}
