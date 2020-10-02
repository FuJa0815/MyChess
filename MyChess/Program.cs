using System;
using MyChess.Ai;
using MyChess.OutputClasses;

namespace MyChess
{
    internal class Program
    {
        public static Actor PlayerW { get; } = new HumanActor(PlayerColor.White);
        public static Actor PlayerB { get; } = new RandomAiActor(PlayerColor.Black);

        private static void Main()
        {
            ConsoleFontHelper.Init();
            ChessBoard.CurrentBoard.Render();
            ChessBoard.CurrentBoard.Pieces.ForEach(p => p.Render());
            while (true)
            {
                try
                {
                    var b = ChessBoard.CurrentBoard;
                    RoundManager.MakeRound(ref b);
                    Output.Out.Text = "";
                } catch(Exception ex)
                {
                    if(RoundManager.CurrentActor.ShowErrors)
                        Output.Out.Text = ex.Message;
                }
            }
        }
    }
}
