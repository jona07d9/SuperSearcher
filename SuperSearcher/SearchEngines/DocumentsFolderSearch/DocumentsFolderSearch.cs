using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSearcher.SearchEngines.DocumentsFolderSearch
{
    /// <summary>
    /// Searches the documents folder.
    /// </summary>
    public class DocumentsFolderSearch : ISearchEngine
    {
        /// <summary>
        /// The path of the documents folder.
        /// </summary>
        private readonly string DocumentsPath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public string SearchLocationName => "Documents Folder";

        /// <summary>
        /// Searches for files and folders that are in the documents folder or any of it's subfolders.
        /// </summary>
        /// <param name="searchText">The text to search for.</param>
        /// <param name="maxResults">The maximum number of results to return.</param>
        /// <returns>A SearchEngineResults object containing the search results.</returns>
        public Task<SearchEngineResults> Search(string searchText, int maxResults)
        {
            SearchEngineResults searchEngineResults = 
                new() { SearchLocationName = SearchLocationName };

            foreach (string entry in Directory.EnumerateFileSystemEntries(
                DocumentsPath, $"*{searchText}*", SearchOption.AllDirectories))
            {
                if (searchEngineResults.SearchResults.Count >= maxResults)
                {
                    break;
                }

                string name = entry[(DocumentsPath.Length + 1)..];
                string path = Path.Combine(DocumentsPath, name);
                searchEngineResults.SearchResults.Add(
                    new DocumentsFolderSearchResult() { Name = name, Path = path});
            }

            return Task.FromResult(searchEngineResults);
        }
    }
}
