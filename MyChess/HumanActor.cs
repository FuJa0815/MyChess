using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using MyChess.Exceptions;

namespace MyChess
{
    public class HumanActor : Actor
    {
        public HumanActor(PlayerColor color) : base(color)
        {
        }

        private Regex r = new Regex(@"^[a-h][1-8]-[a-h][1-8]$");
        public override (ChessPosition from, ChessPosition to) CalculateMove()
        {
            Console.SetCursorPosition(0, 18);
            Console.Write($"{RoundManager.CurrRound}. Zug {(Color==PlayerColor.WHITE?"weiß":"schwarz")}: ");
            string line = "";
            do
            {
                line = Console.ReadLine().Trim();
            } while (!r.IsMatch(line));
            var splits = line.Split('-');
            var from = new ChessPosition(byte.Parse(""+splits[0][0]), (byte)('h' - splits[0][1]));
            var to = new ChessPosition(byte.Parse("" + splits[1][0]), (byte)('h' - splits[1][1]));
            return (from, to);
        }
    }
}
