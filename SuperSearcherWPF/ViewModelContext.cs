using SuperSearcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSearcherWPF
{
    /// <summary>
    /// Contains information shared between view models.
    /// </summary>
    public class ViewModelContext
    {
        /// <summary>
        /// Contains statistics about the searches made.
        /// </summary>
        public SearchStatistics SearchStatistics { get; set; } = new()
        {
            SearchSaver = new FileStringSaver()
            {
                SaveName = "searches"
            }
        };
    }
}
