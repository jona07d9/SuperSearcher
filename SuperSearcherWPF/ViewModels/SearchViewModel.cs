using SuperSearcher;
using SuperSearcher.SearchEngines;
using SuperSearcher.SearchEngines.GoogleBooks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SuperSearcherWPF.ViewModels
{
    /// <summary>
    /// Contains search results and the name of the search engine they came from.
    /// </summary>
    public class SearchEngineResults
    {
        /// <summary>
        /// The name of the search engine the search results came from.
        /// </summary>
        public string EngineName { get; set; }
        /// <summary>
        /// The search results returned by the search engine.
        /// </summary>
        public List<string> Results { get; set; }

        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="engineName">The name of the search engine the search results came from.</param>
        /// <param name="results">The search results returned by the search engine.</param>
        public SearchEngineResults(string engineName, List<string> results)
        {
            EngineName = engineName;
            Results = results;
        }
    }

    /// <summary>
    /// View model for Search.xaml.
    /// </summary>
    public class SearchViewModel : ViewModel
    {
        /// <summary>
        /// The maximum number of search results from each search engine.
        /// </summary>
        private const int MaxSearchResultsPerEngine = 3;

        /// <summary>
        /// The search engines used when searching.
        /// </summary>
        private List<ISearchEngine> _searchEngines = new() 
        { 
            new DocumentsFolderSearch(),
            new GoogleBooksAPI()
        };
        /// <summary>
        /// The text in the search text box.
        /// </summary>
        private string _searchText = "";
        /// <summary>
        /// Command that executes a search.
        /// </summary>
        private ICommand _searchCommand;
        /// <summary>
        /// The search results from the last search.
        /// </summary>
        private List<SearchEngineResults> _searchResults = new();

        /// <summary>
        /// The text in the search text box.
        /// </summary>
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (value != _searchText)
                {
                    _searchText = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Command that executes a search.
        /// </summary>
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(
                        async parameter =>
                        {
                            _context.SearchStatistics.AddSearch(SearchText);

                            List<SearchEngineResults> newResults = new();
                            foreach (ISearchEngine searchEngine in _searchEngines)
                            {
                                // TODO: Start all search engines and await them all at once.
                                List<string> results = await searchEngine.Search(SearchText, MaxSearchResultsPerEngine);
                                SearchEngineResults engineResults = new(searchEngine.SearchLocationName, results);
                                newResults.Add(engineResults);
                            }
                            SearchResults = newResults;
                        },

                        parameter =>
                        {
                            return SearchText.Length > 0;
                        }
                    );
                }

                return _searchCommand;
            }
        }

        /// <summary>
        /// The search results from the last search.
        /// </summary>
        public List<SearchEngineResults> SearchResults
        {
            get => _searchResults;
            set
            {
                if (value != _searchResults)
                {
                    _searchResults = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Calls the base constructor to initialize the <see cref="ViewModelContext"/>
        /// </summary>
        /// <param name="context">Contains information shared between view models.</param>
        public SearchViewModel(ViewModelContext context) : base(context)
        {

        }
    }
}
