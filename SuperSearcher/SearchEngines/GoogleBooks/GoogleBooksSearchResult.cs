namespace SuperSearcher.SearchEngines.GoogleBooks
{
    /// <summary>
    /// A search result from the Google Books API.
    /// </summary>
    public class GoogleBooksSearchResult : ISearchResult
    {
        /// <summary>
        /// The title of the book.
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// A link to the Google Books page for this book.
        /// </summary>
        public string Link { get; init; } = "";

        /// <summary>
        /// Opens the book's Google Books link.
        /// </summary>
        public void Open()
        {
            // Link must be wrapped with " " or it doesn't open correctly.
            _ = System.Diagnostics.Process.Start("explorer.exe", $"\"{Link}\"");
        }
    }
}
