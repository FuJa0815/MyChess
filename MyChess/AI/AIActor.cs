using System;
using MyChess.OutputClasses;

namespace MyChess.Ai
{
    public abstract class AiActor : Actor
    {
        public override bool ShowErrors => false;

        protected AiActor(PlayerColor color) : base(color)
        {

        }

        protected void PrintLoading()
        {
            Console.SetCursorPosition(0, 18);
            ConsoleFontHelper.ClearCurrentConsoleLine();
            Console.Write("Calculating...");
        }
    }
}