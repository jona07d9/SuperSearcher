using SuperSearcher;
using SuperSearcher.SearchEngines.DocumentsFolderSearch;
using SuperSearcher.SearchEngines.GoogleBooks;
using System.Collections.Generic;
using System.Windows.Input;

namespace SuperSearcherWPF.ViewModels
{
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
        private readonly List<ISearchEngine> _searchEngines = new() 
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
        private List<SearchEngineResultsViewModel> _searchResults = new();

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

                            List<SearchEngineResultsViewModel> newResults = new();
                            foreach (ISearchEngine searchEngine in _searchEngines)
                            {
                                // TODO: Start all search engines and await them all at once.
                                List<ISearchResult> results = await searchEngine.Search(SearchText, MaxSearchResultsPerEngine);
                                SearchEngineResultsViewModel engineResults = new(_context, searchEngine.SearchLocationName, results);
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
        public List<SearchEngineResultsViewModel> SearchResults
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
