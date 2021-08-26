using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSearcher.SearchEngines
{
    /// <summary>
    /// Searches the documents folder.
    /// </summary>
    public class DocumentsFolderSearch : ISearchEngine
    {
        /// <summary>
        /// The path of the documents folder.
        /// </summary>
        private readonly string DocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public string SearchLocationName => "Documents Folder";

        /// <summary>
        /// Searches for files and folders that are directly in the documents folder.
        /// </summary>
        /// <param name="searchText">The text to search for.</param>
        /// <param name="maxResults">The maximum number of results to return.</param>
        /// <returns>The names of all files and folders that contains the search text.</returns>
        public Task<List<string>> Search(string searchText, int maxResults)
        {
            List<string> results = new();

            foreach (string entry in Directory.EnumerateFileSystemEntries(DocumentsPath, $"*{searchText}*"))
            {
                if (results.Count >= maxResults)
                {
                    break;
                }

                string entryName = entry[(DocumentsPath.Length + 1)..];
                results.Add(entryName);
            }

            return Task.FromResult(results);
        }
    }
}
