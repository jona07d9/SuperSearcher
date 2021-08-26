using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSearcher
{
    /// <summary>
    /// Interface for saving and loading strings.
    /// </summary>
    public interface IStringSaver
    {
        /// <summary>
        /// A unique name for this save.
        /// </summary>
        string SaveName { get; set; }

        /// <summary>
        /// Saves a list of strings, overwriting previously saved strings.
        /// </summary>
        /// <param name="strings">The strings to save.</param>
        void Save(List<string> strings);

        /// <summary>
        /// Loads a list of strings from somewhere.
        /// </summary>
        /// <returns>The strings that was loaded.</returns>
        Task<List<string>> Load();
    }
}
