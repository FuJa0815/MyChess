using System;
using MyChess.AI;
using MyChess.OutputClasses;

namespace MyChess.Ai
{
    public class RandomAiActor : AiActor
    {
        private readonly Random _r = new Random();
        public RandomAiActor(PlayerColor color) : base(color)
        {

        }
        public override ChessMove CalculateMove()
        {
            var list = ChessBoard.CurrentBoard.CalculateAllPossibleMoves(Color);
            return list[_r.Next(list.Count)];
        }
    }
}
