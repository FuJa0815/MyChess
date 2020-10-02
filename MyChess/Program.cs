using System;
using MyChess.OutputClasses;

namespace MyChess
{
    class Program
    {
        public static Actor PlayerW { get; } = new HumanActor(PlayerColor.WHITE);
        public static Actor PlayerB { get; } = new HumanActor(PlayerColor.BLACK);

        static void Main(string[] args)
        {
            ConsoleFontHelper.Init();
            ChessBoard.CBoard.Render();
            ChessBoard.CBoard.Board.ForEach(p => p.Render());
            while (true)
            {
                try
                {
                    RoundManager.MakeRound();
                    Output.Out.Text = "";
                } catch(Exception ex)
                {
                    Output.Out.Text = ex.Message;
                }
            }
        }
    }
}
