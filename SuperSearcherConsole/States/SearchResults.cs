using SuperSearcher;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSearcherConsole.States
{
    /// <summary>
    /// Displays search results.
    /// </summary>
    public class SearchResults : State
    {
        /// <summary>
        /// Search engine names and it's results.
        /// </summary>
        private readonly List<SearchEngineResults> _searchEngineResults;

        /// <summary>
        /// Converts an item number to a search result.
        /// </summary>
        /// <param name="itemNumber">The item's number in the list of search results.</param>
        /// <returns>The search result that has the same item number in the list of search results.</returns>
        private ISearchResult ItemNumberToSearchResult(int itemNumber)
        {
            ISearchResult searchResult = null;

            int counter = 0;
            foreach (SearchEngineResults searchEngineResults in _searchEngineResults)
            {
                if (counter + searchEngineResults.SearchResults.Count < itemNumber)
                {
                    counter += searchEngineResults.SearchResults.Count;
                }
                else
                {
                    int index = itemNumber - counter - 1;
                    searchResult = searchEngineResults.SearchResults[index];
                    break;
                }
            }

            return searchResult;
        }

        /// <summary>
        /// Gets the total number of search results.
        /// </summary>
        /// <returns>The total number of search results.</returns>
        private int GetSearchResultCount()
        {
            int count = 0;
            foreach (SearchEngineResults searchEngineResults in _searchEngineResults)
            {
                count += searchEngineResults.SearchResults.Count;
            }

            return count;
        }

        /// <summary>
        /// Creates commands and initializes search results.
        /// </summary>
        /// <param name="context">Information shared between states.</param>
        /// <param name="searchEngineResults">Search engine names and it's results.</param>
        public SearchResults(StateContext context, List<SearchEngineResults> searchEngineResults) : base(context)
        {
            Commands.Add("menu", ("Gå tilbage til menuen.", () => Task.FromResult<State>(new Menu(Context))));
            Commands.Add("søg", ("Lav endnu en søgning.", () => Task.FromResult<State>(new Search(Context))));
            _searchEngineResults = searchEngineResults;
        }

        /// <summary>
        /// Checks if the user inputted a number that matches a search result.
        /// </summary>
        /// <param name="input">The text to be processed.</param>
        /// <returns>The state after the input has been processed.</returns>
        public override Task<State> Process(string input)
        {
            if (int.TryParse(input, out int number) && number >= 1 && number <= GetSearchResultCount())
            {
                ISearchResult searchResult = ItemNumberToSearchResult(number);
                searchResult.Open();
            }

            return base.Process(input);
        }

        /// <summary>
        /// Displays each search engine's name and it's search results.
        /// </summary>
        public override void DisplayMessage()
        {
            Console.WriteLine("Søgeresultater" + Environment.NewLine);

            int counter = 1;
            foreach (SearchEngineResults searchEngineResults in _searchEngineResults)
            {
                Console.WriteLine($"{searchEngineResults.Identifier}:");

                for (int i = 0; i < searchEngineResults.SearchResults.Count; i++)
                {
                    Console.WriteLine($"  {counter++}. {searchEngineResults.SearchResults[i].Name}");
                }
            }
        }

        /// <summary>
        /// Displays a message after showing the commands.
        /// </summary>
        public override void DisplayCommands()
        {
            base.DisplayCommands();
            Console.WriteLine("Eller indtast nummeret på et søgeresultat, for at åbne det.");
            Console.WriteLine();
        }
    }
}
