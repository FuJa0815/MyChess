using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyChess.OutputClasses;

namespace MyChess.AI
{
    public class AI : Actor
    {
        public AI(PlayerColor color) : base(color)
        {

        }

        public override ChessMove CalculateMove()
        {
            throw new NotImplementedException();
        }
    }
}