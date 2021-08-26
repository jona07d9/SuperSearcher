using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSearcherConsole.States
{
    /// <summary>
    /// Displays statistics.
    /// </summary>
    public class Statistics : State
    {
        /// <summary>
        /// The number of characters to show in the most/least used characters list.
        /// </summary>
        private const int UsedCharactersCount = 3;

        /// <summary>
        /// Adds commands.
        /// </summary>
        /// <param name="context"></param>
        public Statistics(StateContext context) : base(context)
        {
            Commands.Add("1", ("Menu", () => Task.FromResult<State>(new Menu(Context))));
        }

        /// <summary>
        /// Displays the statistics.
        /// </summary>
        public override void DisplayMessage()
        {
            Console.WriteLine("Statistik");
            Console.WriteLine();

            Console.WriteLine(
                $"Antal søgninger: {Context.SearchStatistics.TotalSearches}" + Environment.NewLine +
                $"Længste søgning: {Context.SearchStatistics.LongestSearch}" + Environment.NewLine +
                $"Korteste søgning: {Context.SearchStatistics.ShortestSearch}" + Environment.NewLine +
                $"Gennemsnitslængde: {Context.SearchStatistics.AverageLength}" + Environment.NewLine
            );

            List<KeyValuePair<char, int>> characterCounts = Context.SearchStatistics.GetSortedCharacterCounts();
            int actualCount = Math.Min(characterCounts.Count, UsedCharactersCount);

            Console.WriteLine($"Mest brugte tegn:");

            foreach ((char character, int count) in characterCounts.Take(actualCount))
            {
                Console.WriteLine($"  {character}: {count}");
            }

            Console.WriteLine();
            Console.WriteLine($"Mindst brugte tegn:");

            foreach ((char character, int count) in characterCounts.TakeLast(actualCount))
            {
                Console.WriteLine($"  {character}: {count}");
            }
        }
    }
}
