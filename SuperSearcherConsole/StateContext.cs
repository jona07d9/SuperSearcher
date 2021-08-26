using SuperSearcher;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSearcherConsole
{
    /// <summary>
    /// Contains information shared between states.
    /// </summary>
    public class StateContext
    {
        /// <summary>
        /// The name of the file searches are saved to.
        /// </summary>
        private const string SearchesFileName = "searches.txt";
        /// <summary>
        /// The name of the folder application data is saved in.
        /// </summary>
        private const string ApplicationDataFolderName = "SuperSearcher";
        /// <summary>
        /// The full path of the application data folder.
        /// </summary>
        private static readonly string ApplicationDataFolderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            ApplicationDataFolderName);
        /// <summary>
        /// The full path of the searches file.
        /// </summary>
        private static readonly string SearchesFilePath = Path.Combine(
            ApplicationDataFolderPath, SearchesFileName);

        /// <summary>
        /// Calculates statistics about searches.
        /// </summary>
        public SearchStatistics SearchStatistics { get; set; } = new();

        /// <summary>
        /// Saves the searches used for statistics to a file.
        /// </summary>
        public async Task SaveStatisticsSearches()
        {
            Directory.CreateDirectory(ApplicationDataFolderPath);
            await File.WriteAllLinesAsync(SearchesFilePath, SearchStatistics.GetSearches());
        }

        /// <summary>
        /// Loads searches from a file and adds them to the statistics.
        /// </summary>
        public async Task LoadStatisticsSearches()
        {
            if (!File.Exists(SearchesFilePath))
            {
                return;
            }

            string[] searches = await File.ReadAllLinesAsync(SearchesFilePath);

            for (int i = 0; i < searches.Length; i++)
            {
                SearchStatistics.AddSearch(searches[i]);
            }
        }
    }
}
