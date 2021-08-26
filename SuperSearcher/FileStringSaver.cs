using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSearcher
{
    /// <summary>
    /// Saves and loads strings to and from a file.
    /// </summary>
    public class FileStringSaver : IStringSaver
    {
        /// <summary>
        /// The file extension of the file where the strings are saved.
        /// </summary>
        private const string FileExtension = ".txt";
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
        /// The name of the file to save strings to.
        /// </summary>
        public string SaveName { get; set; } = "strings";

        /// <summary>
        /// Returns the full path of the file where strings are saved to.
        /// </summary>
        /// <returns>The full path of the file where strings are saved to.</returns>
        private string GetFilePath()
        {
            return Path.Combine(ApplicationDataFolderPath, SaveName + FileExtension);
        }

        /// <summary>
        /// Saves a list of strings to a file, overwriting previously saved strings.
        /// </summary>
        /// <param name="strings">The strings to save.</param>
        public async void Save(List<string> strings)
        {
            _ = Directory.CreateDirectory(ApplicationDataFolderPath);
            await File.WriteAllLinesAsync(GetFilePath(), strings);
        }

        /// <summary>
        /// Loads strings from a file.
        /// </summary>
        /// <returns>The strings that was loaded.</returns>
        public async Task<List<string>> Load()
        {
            if (!File.Exists(GetFilePath()))
            {
                return new();
            }

            return new(await File.ReadAllLinesAsync(GetFilePath()));
        }
    }
}
