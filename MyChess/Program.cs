using System;

namespace MyChess
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleFontHelper.Init();
            ChessBoard cb = new ChessBoard();
            cb.Render();
            cb.Board.ForEach(p=>p.Render());
            Console.ReadKey();
        }
    }
}
