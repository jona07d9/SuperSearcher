namespace SuperSearcher
{
    /// <summary>
    /// Interface for search results.
    /// </summary>
    public interface ISearchResult
    {
        /// <summary>
        /// The name of the thing the search result contains.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Opens the search result.
        /// </summary>
        public void Open();
    }
}
