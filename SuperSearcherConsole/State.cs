using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSearcherConsole
{
    /// <summary>
    /// Base class for states.
    /// </summary>
    public abstract class State
    {
        /// <summary>
        /// Contains information shared between states.
        /// </summary>
        protected StateContext Context { get; set; }

        /// <summary>
        /// Command name and action indexed by command text.
        /// </summary>
        protected Dictionary<string, (string, Func<Task<State>>)>
            Commands { get; set; } = new();

        /// <summary>
        /// Executes a command if it exists.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>The state after the command has executed.</returns>
        private async Task<State> ExecuteCommand(string command)
        {
            State newState = this;

            string lowerCaseCommand = command.ToLower(
                System.Globalization.CultureInfo.CurrentCulture);
            if (Commands.ContainsKey(lowerCaseCommand))
            {
                (string name, Func<Task<State>> execute) =
                    Commands[lowerCaseCommand];
                newState = await execute.Invoke();
            }

            return newState;
        }

        /// <summary>
        /// Processes the input not as a command.
        /// </summary>
        /// <param name="input">The text to be processed.</param>
        /// <returns>The state after processing.</returns>
        protected virtual Task<State> ProcessInput(string input)
        {
            return Task.FromResult(this);
        }

        /// <summary>
        /// Initializes the state context.
        /// </summary>
        /// <param name="context">Information shared between states.</param>
        public State(StateContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Displays the state's message and commands.
        /// </summary>
        public void Display()
        {
            DisplayMessage();
            DisplayCommands();
        }

        /// <summary>
        /// Decides whether or not the input should be processed as a command,
        /// depending on if the state has any commands.
        /// </summary>
        /// <param name="input">The text to be processed.</param>
        /// <returns>The state after the input has been processed.</returns>
        public virtual async Task<State> Process(string input)
        {
            State newState = Commands.Count > 0 ?
                await ExecuteCommand(input) : await ProcessInput(input);
            return newState;
        }

        /// <summary>
        /// Displays the state's message.
        /// </summary>
        public abstract void DisplayMessage();

        /// <summary>
        /// Displays a list of the state's commands.
        /// </summary>
        public void DisplayCommands()
        {
            if (Commands.Count == 0)
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Kommandoer:");

            foreach (KeyValuePair<string, (string, Func<Task<State>>)>
                command in Commands)
            {
                (string description, _) = command.Value;
                Console.WriteLine($"  {command.Key}: {description}");
            }

            Console.WriteLine();
        }
    }
}
