using SuperSearcherConsole.States;
using System;
using System.Threading.Tasks;

namespace SuperSearcherConsole
{
    /// <summary>
    /// Console program for searching and displaying search statistics.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Starts the program in the <see cref="Menu"/>
        /// </summary>
        /// <returns>A <see cref="Task"/></returns>
        private static async Task Main()
        {
            StateContext context = new();
            await context.LoadStatisticsSearches();
            State state = new Menu(context);

            while (state != null)
            {
                state.Display();
                state = await state.Process(Console.ReadLine());
                Console.Clear();
            }
        }
    }
}
