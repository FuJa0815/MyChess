using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyChess.OutputClasses;

namespace MyChess.AI
{
    public class AIActor : Actor
    {
        Random r = new Random();
        public AIActor(PlayerColor color) : base(color)
        {

        }

        public override bool ShowErrors => false;

        public override ChessMove CalculateMove()
        {
            var list = AIChessBoardHelper.CalculateAllPossibleMoves(ChessBoard.CurrentBoard, Color);
            return list[r.Next(list.Count)];
        }
    }
}