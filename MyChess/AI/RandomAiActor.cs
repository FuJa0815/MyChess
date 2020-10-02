using System;
using System.Collections.Generic;
using System.Text;
using MyChess.OutputClasses;

namespace MyChess.AI
{
    public class RandomAiActor : AIActor
    {
        private readonly Random _r = new Random();
        public RandomAiActor(PlayerColor color) : base(color)
        {

        }
        public override ChessMove CalculateMove()
        {
            var list = AIChessBoardHelper.CalculateAllPossibleMoves(ChessBoard.CurrentBoard, Color);
            return list[_r.Next(list.Count)];
        }
    }
}
