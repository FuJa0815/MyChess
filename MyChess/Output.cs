using System;
using System.Collections.Generic;
using System.Text;

namespace MyChess
{
    public class Output : IRender
    {
        private static Output _out;
        public static Output Out => _out ??= new Output();
        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                Render();
            }
        }
        public void Render()
        {
            Console.SetCursorPosition(0, 18);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write(Text);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
