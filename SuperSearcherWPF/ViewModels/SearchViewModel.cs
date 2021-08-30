using SuperSearcher;
using SuperSearcher.SearchEngines.DocumentsFolderSearch;
using SuperSearcher.SearchEngines.GoogleBooks;
using System.Collections.Generic;
using System.Threading.Tasks;
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

                            List<Task> searchTasks = new();
                            foreach (ISearchEngine searchEngine in _searchEngines)
                            {
                                Task<SearchEngineResults> searchTask = searchEngine.Search(SearchText, MaxSearchResultsPerEngine);
                                searchTasks.Add(searchTask);
                            }

                            List<SearchEngineResultsViewModel> newResults = new();
                            while (searchTasks.Count > 0)
                            {
                                Task<SearchEngineResults> searchTask = await Task.WhenAny(searchTasks) as Task<SearchEngineResults>;
                                SearchEngineResults searchEngineResults = searchTask.Result;

                                SearchEngineResultsViewModel engineResultsViewModel = new(
                                    _context, searchEngineResults.SearchLocationName, searchEngineResults.SearchResults);
                                newResults.Add(engineResultsViewModel);

                                _ = searchTasks.Remove(searchTask);
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
