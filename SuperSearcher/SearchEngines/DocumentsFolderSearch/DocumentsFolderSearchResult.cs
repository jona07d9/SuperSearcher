using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSearcher.SearchEngines.DocumentsFolderSearch
{
    /// <summary>
    /// A search result from searching the documents folder.
    /// </summary>
    public class DocumentsFolderSearchResult : ISearchResult
    {
        /// <summary>
        /// The name of the file or folder the search result contains.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The full path of the file or folder.
        /// </summary>
        public string Path { get; init; }

        /// <summary>
        /// Opens the file or folder.
        /// </summary>
        public void Open()
        {
            _ = System.Diagnostics.Process.Start("explorer.exe", Path);
        }
    }
}
