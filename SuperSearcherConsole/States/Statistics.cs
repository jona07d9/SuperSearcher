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
        /// The maximum number of items to show in statistic lists.
        /// </summary>
        private const int MaxItemsShown = 3;

        /// <summary>
        /// Adds commands.
        /// </summary>
        /// <param name="context">Information shared between states.</param>
        public Statistics(StateContext context) : base(context)
        {
            Commands.Add("menu", ("Gå tilbage til menuen.", () => Task.FromResult<State>(new Menu(Context))));
            Commands.Add("nulstil", ("Slet alle søgninger og nulstil statistikken.", () => {
                Context.SearchStatistics.ResetStatistics();
                return Task.FromResult<State>(this);
            }));
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

            Console.WriteLine("Seneste søgninger:");

            int itemNumber = 1;
            foreach (string search in Context.SearchStatistics.GetLatestSearches(MaxItemsShown))
            {
                Console.WriteLine($"  {itemNumber++}. {search}");
            }

            Console.WriteLine();
            Console.WriteLine($"Mest brugte tegn:");

            foreach ((char character, int count) in
                Context.SearchStatistics.GetMostUsedCharacters(MaxItemsShown))
            {
                Console.WriteLine($"  {character}: {count}");
            }

            Console.WriteLine();
            Console.WriteLine($"Mindst brugte tegn:");

            foreach ((char character, int count) in
                Context.SearchStatistics.GetLeastUsedCharacters(MaxItemsShown))
            {
                Console.WriteLine($"  {character}: {count}");
            }
        }
    }
}
