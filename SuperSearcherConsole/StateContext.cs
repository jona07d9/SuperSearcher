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
        /// Calculates statistics about searches.
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
