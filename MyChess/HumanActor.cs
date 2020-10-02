using System;
using System.Text.RegularExpressions;
using MyChess.OutputClasses;

namespace MyChess
{
    public class HumanActor : Actor
    {
        public HumanActor(PlayerColor color) : base(color)
        {
        }

        private readonly Regex _r = new Regex(@"^[a-h][1-8]-[a-h][1-8]$");

        public override bool ShowErrors => true;

        public override ChessMove CalculateMove()
        {
            string line;
            var    notFirst = false;
            do
            {
                if (notFirst)
                    Output.Out.Text = "Format: a1-a2";
                Console.SetCursorPosition(0, 18);
                ConsoleFontHelper.ClearCurrentConsoleLine();
                Console.Write($"{RoundManager.CurrentRound}. Zug {(Color == PlayerColor.White ? "weiß" : "schwarz")}: ");
                line     = Console.ReadLine()?.Trim();
                notFirst = true;
            } while (line == null || !_r.IsMatch(line));
            Output.Out.Text = "";
            var splits = line.Split('-');
            var from = new ChessPosition((byte)(splits[0][0]-96), byte.Parse("" + splits[0][1]));
            var to = new ChessPosition((byte)(splits[1][0]-96), byte.Parse("" + splits[1][1]));
            return new ChessMove(from, to);
        }
    }
}
