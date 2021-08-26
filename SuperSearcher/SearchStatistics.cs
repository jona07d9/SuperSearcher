using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSearcher
{
    /// <summary>
    /// Calculates statistics for a list of search texts.
    /// </summary>
    public class SearchStatistics
    {
        /// <summary>
        /// The IStringSaver used to save and load searches.
        /// </summary>
        private IStringSaver _searchSaver;
        /// <summary>
        /// The searches included in the statistics.
        /// </summary>
        private readonly List<string> _searches = new();
        /// <summary>
        /// How many times each character has been used in a search.
        /// </summary>
        private readonly Dictionary<char, int> _characterCounts = new();
        /// <summary>
        /// The total length of all searches combined.
        /// </summary>
        private int _totalLength;

        /// <summary>
        /// The IStringSaver used to save and load searches.
        /// </summary>
        public IStringSaver SearchSaver
        {
            get => _searchSaver;
            set
            {
                _searchSaver = value;
                LoadSearches();
            }
        }

        /// <summary>
        /// The total amount of searches made.
        /// </summary>
        public int TotalSearches => _searches.Count;

        /// <summary>
        /// The search text that was the longest.
        /// </summary>
        public string LongestSearch { get; private set; } = "";

        /// <summary>
        /// The search text that was the shortest.
        /// </summary>
        public string ShortestSearch { get; private set; } = "";

        /// <summary>
        /// The average length of searches.
        /// </summary>
        public int AverageLength { get; private set; }

        /// <summary>
        /// Called when a new search has been included in the statistics.
        /// </summary>
        public event EventHandler SearchAdded;

        /// <summary>
        /// Loads searches using SearchSaver.
        /// </summary>
        private async void LoadSearches()
        {
            if (SearchSaver == null)
            {
                return;
            }

            List<string> searches = await SearchSaver.Load();
            foreach (string search in searches)
            {
                AddSearch(search);
            }
        }

        /// <summary>
        /// Calls the SearchAdded event.
        /// </summary>
        /// <param name="e">Information about the event.</param>
        protected virtual void OnSearchAdded(EventArgs e)
        {
            // Copy the event to avoid a race condition
            // if the last subscriber unsubscribes
            // after the null check, but before the event is raised.
            EventHandler handler = SearchAdded;
            handler?.Invoke(this, e);
        }

        /// <summary>
        /// Includes a search in the statistics.
        /// </summary>
        /// <param name="searchText">The search text to include.</param>
        public void AddSearch(string searchText)
        {
            _searches.Add(searchText);
            SearchSaver?.Save(_searches);

            foreach (char character in searchText)
            {
                if (_characterCounts.ContainsKey(character))
                {
                    _characterCounts[character]++;
                }
                else
                {
                    _characterCounts[character] = 1;
                }
            }

            if (searchText.Length > LongestSearch.Length)
            {
                LongestSearch = searchText;
            }

            if (ShortestSearch.Length == 0 || searchText.Length < ShortestSearch.Length)
            {
                ShortestSearch = searchText;
            }

            _totalLength += searchText.Length;
            AverageLength = _totalLength / _searches.Count;
            OnSearchAdded(EventArgs.Empty);
        }

        /// <summary>
        /// Returns how many times the character has been used in a search.
        /// </summary>
        /// <param name="character">The character to get the count of.</param>
        /// <returns>How many times the character has been used in a search.</returns>
        public int GetCharacterCount(char character)
        {
            if (!_characterCounts.ContainsKey(character))
            {
                return 0;
            }

            return _characterCounts[character];
        }

        /// <summary>
        /// Gets a list of characters used in a search and how many times it was used, sorted by how many times it was used.
        /// </summary>
        /// <returns>A list of character and used count key value pairs.</returns>
        public List<KeyValuePair<char, int>> GetSortedCharacterCounts()
        {
            List<KeyValuePair<char, int>> characterCounts = _characterCounts.ToList();
            characterCounts.Sort(
                    delegate (KeyValuePair<char, int> x, KeyValuePair<char, int> y)
                    {
                        return y.Value - x.Value;
                    });

            return characterCounts;
        }

        /// <summary>
        /// Gets the most used characters and how many times they were used.
        /// </summary>
        /// <param name="maxCharacters">The maximum number of characters to return.</param>
        /// <returns>A list of character, count KeyValuePairs.</returns>
        public List<KeyValuePair<char, int>> GetMostUsedCharacters(int maxCharacters)
        {
            return (from entry in _characterCounts
                    orderby entry.Value descending
                    select entry).Take(maxCharacters).ToList();
        }

        /// <summary>
        /// Gets the least used characters and how many times they were used.
        /// </summary>
        /// <param name="maxCharacters">The maximum number of characters to return.</param>
        /// <returns>A list of character, count KeyValuePairs.</returns>
        public List<KeyValuePair<char, int>> GetLeastUsedCharacters(int maxCharacters)
        {
            return (from entry in _characterCounts
                    orderby entry.Value ascending
                    select entry).Take(maxCharacters).ToList();
        }

        /// <summary>
        /// Get a read only list of searches.
        /// </summary>
        /// <returns>A list of searches that can't be modified.</returns>
        public ReadOnlyCollection<string> GetSearches()
        {
            return _searches.AsReadOnly();
        }
    }
}
