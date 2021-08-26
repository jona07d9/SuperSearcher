using System;
using System.Collections.Generic;
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
        private readonly List<(string, List<string>)> _searchEngineResults = null;

        /// <summary>
        /// Creates commands and initializes search results.
        /// </summary>
        /// <param name="context">Information shared between states.</param>
        /// <param name="searchEngineResults">Search engine names and it's results.</param>
        public SearchResults(StateContext context, List<(string, List<string>)> searchEngineResults) : base(context)
        {
            Commands.Add("menu", ("Gå tilbage til menuen.", () => Task.FromResult<State>(new Menu(Context))));
            Commands.Add("søg", ("Lav endnu en søgning.", () => Task.FromResult<State>(new Search(Context))));
            _searchEngineResults = searchEngineResults;
        }

        /// <summary>
        /// Displays each search engine's name and it's search results.
        /// </summary>
        public override void DisplayMessage()
        {
            Console.WriteLine("Søgeresultater" + Environment.NewLine);

            int counter = 1;
            foreach ((string searchEngine, List<string> results) in _searchEngineResults)
            {
                Console.WriteLine($"{searchEngine}:");

                for (int i = 0; i < results.Count; i++)
                {
                    Console.WriteLine($"  {counter++}. {results[i]}");
                }
            }
        }
    }
}
