using SuperSearcher;
using SuperSearcher.SearchEngines;
using SuperSearcher.SearchEngines.GoogleBooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        private List<ISearchEngine> _searchEngines = new()
        {
            new DocumentsFolderSearch(),
            new GoogleBooksAPI()
        };

        /// <summary>
        /// Searches for the input text.
        /// </summary>
        /// <param name="input">The search text.</param>
        /// <returns>A state displaying the search results, or the current state, if the search text was empty.</returns>
        protected override async Task<State> ProcessInput(string input)
        {
            if (input.Length > 0)
            {
                List<(string, List<string>)> searchEngineResults = new();

                foreach (ISearchEngine searchEngine in _searchEngines)
                {
                    searchEngineResults.Add((searchEngine.SearchLocationName, await searchEngine.Search(input, MaxResultsPerEngine)));
                }

                Context.SearchStatistics.AddSearch(input);
                await Context.SaveStatisticsSearches();
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
