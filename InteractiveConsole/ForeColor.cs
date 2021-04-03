using System;

namespace InteractiveConsole
{
    public class ForeColor : IDisposable
    {
        private readonly ConsoleColor _previousColor;

        private ForeColor(ConsoleColor color)
        {
            _previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
        }

        public static ForeColor Ask(string question = null)
            => new(Question.AskOneOf<ConsoleColor>(question)); 
        
        public static ForeColor Begin(ConsoleColor color) => new (color);

        public void Dispose() => Console.ForegroundColor = _previousColor;
    }
}