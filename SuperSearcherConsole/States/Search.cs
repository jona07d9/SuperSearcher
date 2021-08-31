using SuperSearcher;
using SuperSearcher.SearchEngines.DocumentsFolderSearch;
using SuperSearcher.SearchEngines.GoogleBooks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperSearcherConsole.States
{
    /// <summary>
    /// Allows the user to input search text.
    /// </summary>
    public class Search : State
    {
        /// <summary>
        /// The maximum number of results to show per search engine.
        /// </summary>
        private const int MaxResultsPerEngine = 3;
        /// <summary>
        /// The search engines that will be used when searching.
        /// </summary>
        private readonly List<ISearchEngine> _searchEngines = new()
        {
            new DocumentsFolderSearch() { ResultsIdentifier = "Mappen Dokumenter" },
            new GoogleBooksAPI() { ResultsIdentifier = "Google Books" }
        };

        /// <summary>
        /// Searches for the input text.
        /// </summary>
        /// <param name="input">The search text.</param>
        /// <returns>A state displaying the search results, or the current state, if the search text was empty.</returns>
        protected override async Task<State> ProcessInput(string input)
        {
            string trimmedInput = input.Trim();
            if (trimmedInput.Length > 0)
            {
                Context.SearchStatistics.AddSearch(trimmedInput);

                List<Task> searchTasks = new();
                foreach (ISearchEngine searchEngine in _searchEngines)
                {
                    Task<SearchEngineResults> searchTask = 
                        searchEngine.Search(trimmedInput, MaxResultsPerEngine);
                    searchTasks.Add(searchTask);
                }

                List<SearchEngineResults> searchEngineResults = new();
                while (searchTasks.Count > 0)
                {
                    Task<SearchEngineResults> searchTask = 
                        await Task.WhenAny(searchTasks) as Task<SearchEngineResults>;
                    searchEngineResults.Add(searchTask.Result);

                    _ = searchTasks.Remove(searchTask);
                }

                return new SearchResults(Context, searchEngineResults);
            }

            return this;
        }

        /// <summary>
        /// Calls the base constructor.
        /// </summary>
        /// <param name="context">Information shared between states.</param>
        public Search(StateContext context) : base(context)
        {

        }

        /// <summary>
        /// Displays a message that tells the user to type the search text.
        /// </summary>
        public override void DisplayMessage()
        {
            Console.Write("Indtast søgetekst: ");
        }
    }
}
