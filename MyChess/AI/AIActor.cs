using System;
using MyChess.OutputClasses;

namespace MyChess.AI
{
    public abstract class AiActor : Actor
    {
        public override bool ShowErrors => false;

        protected AiActor(PlayerColor color) : base(color)
        {
            Console.SetCursorPosition(0, 18);
            ConsoleFontHelper.ClearCurrentConsoleLine();
            Console.Write("Calculating...");
        }
    }
}