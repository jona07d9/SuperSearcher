using System;
using System.Threading.Tasks;

namespace SuperSearcherConsole.States
{
    /// <summary>
    /// Allows the user to select which action they want to do.
    /// </summary>
    public class Menu : State
    {
        /// <summary>
        /// Adds commands.
        /// </summary>
        public Menu(StateContext context) : base(context)
        {
            Commands.Add("søg", ("Lav en søgning.", () => Task.FromResult<State>(new Search(Context))));
            Commands.Add("statistik", ("Se søgestatistik.", () => Task.FromResult<State>(new Statistics(Context))));
            Commands.Add("afslut", ("Afslut programmet.", () => Task.FromResult<State>(null)));
        }

        /// <summary>
        /// Displays the menu title.
        /// </summary>
        public override void DisplayMessage()
        {
            Console.WriteLine("Menu");
        }
    }
}
