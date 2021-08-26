using SuperSearcher;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSearcherWPF.ViewModels
{
    /// <summary>
    /// View model for Statistics.xaml.
    /// </summary>
    public class StatisticsViewModel : ViewModel
    {
        /// <summary>
        /// The total number of searches that have been done.
        /// </summary>
        private int _totalSearches;
        /// <summary>
        /// The longest text that was searched for.
        /// </summary>
        private string _longestSearch = "";
        /// <summary>
        /// The shortest text that was searched for.
        /// </summary>
        private string _shortestSearch = "";
        /// <summary>
        /// The average length of all searches.
        /// </summary>
        private int _averageLength;
        /// <summary>
        /// The most used characters and how many times they were used.
        /// </summary>
        private List<KeyValuePair<char, int>> _mostUsedCharacters = new();
        /// <summary>
        /// The least used characters and how many times they were used.
        /// </summary>
        private List<KeyValuePair<char, int>> _leastUsedCharacters = new();

        /// <summary>
        /// The total number of searches that have been done.
        /// </summary>
        public int TotalSearches
        {
            get => _totalSearches;
            set
            {
                if (value != _totalSearches)
                {
                    _totalSearches = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// The longest text that was searched for.
        /// </summary>
        public string LongestSearch
        {
            get => _longestSearch;
            set
            {
                if (value != _longestSearch)
                {
                    _longestSearch = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// The shortest text that was searched for.
        /// </summary>
        public string ShortestSearch
        {
            get => _shortestSearch;
            set
            {
                if (value != _shortestSearch)
                {
                    _shortestSearch = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// The average length of all searches.
        /// </summary>
        public int AverageSearchLength
        {
            get => _averageLength;
            set
            {
                if (value != _averageLength)
                {
                    _averageLength = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// The most used characters and how many times they were used.
        /// </summary>
        public List<KeyValuePair<char, int>> MostUsedCharacters
        {
            get => _mostUsedCharacters;
            set
            {
                if (value != _mostUsedCharacters)
                {
                    _mostUsedCharacters = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// The least used characters and how many times they were used.
        /// </summary>
        public List<KeyValuePair<char, int>> LeastUsedCharacters
        {
            get => _leastUsedCharacters;
            set
            {
                if (value != _leastUsedCharacters)
                {
                    _leastUsedCharacters = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Updates statistics values.
        /// </summary>
        private void UpdateStatistics()
        {
            TotalSearches = _context.SearchStatistics.TotalSearches;
            LongestSearch = _context.SearchStatistics.LongestSearch;
            ShortestSearch = _context.SearchStatistics.ShortestSearch;
            AverageSearchLength = _context.SearchStatistics.AverageLength;
            MostUsedCharacters = _context.SearchStatistics.GetMostUsedCharacters(3);
            LeastUsedCharacters = _context.SearchStatistics.GetLeastUsedCharacters(3);
        }

        /// <summary>
        /// Calls the base constructor to initialize the view model context./>
        /// </summary>
        /// <param name="context">Contains information shared between view models.</param>
        public StatisticsViewModel(ViewModelContext context) : base(context)
        {
            UpdateStatistics();

            _context.SearchStatistics.SearchAdded += (sender, e) =>
            {
                UpdateStatistics();
            };
        }
    }
}
