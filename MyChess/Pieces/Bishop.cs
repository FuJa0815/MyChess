using System;
using System.Collections.Generic;
using System.Text;
using MyChess.OutputClasses;

namespace MyChess.Pieces
{
    public class Bishop : ChessPiece
    {
        public Bishop(ChessPosition startingPosition, PlayerColor owner) : base(startingPosition, owner)
        {
        }

        public override void RecalculateValidMoves()
        {
            base.RecalculateValidMoves();
            // Look right up
            for(byte i = 1; ; i++)
                if (!CheckAndInsert(i, i)) break;
            // Look left up
            for (byte i = 1; ; i++)
                if (!CheckAndInsert(-i, i)) break;
            // Look right down
            for (byte i = 1; ; i++)
                if (!CheckAndInsert(i, -i)) break;
            // Look left down
            for (byte i = 1; ; i++)
                if (!CheckAndInsert(-i, -i)) break;
        }

        public override char ChessChar => '♗';
    }
}
