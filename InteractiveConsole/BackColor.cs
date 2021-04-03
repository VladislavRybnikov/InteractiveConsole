using System;

namespace InteractiveConsole
{
    public class BackColor : IDisposable
    {
        private readonly ConsoleColor _previousColor;

        private BackColor(ConsoleColor color)
        {
            _previousColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
        }
    
        public static BackColor Ask(string question = null)
            => new(Question.AskOneOf<ConsoleColor>(question));
        
        public static BackColor Begin(ConsoleColor color) => new (color);

        public void Dispose() => Console.BackgroundColor = _previousColor;
    }
}