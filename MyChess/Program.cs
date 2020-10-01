using System;

namespace MyChess
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleFontHelper.Init();
            ChessBoard.CBoard.Render();
            ChessBoard.CBoard.Board.ForEach(p => p.Render());
        }
    }
}
