using System;
using System.Collections.Generic;
using System.Text;

namespace MyChess.Pieces
{
    public class Rook : ChessPiece
    {
        public Rook(ChessPosition startingPosition, PlayerColor owner) : base(startingPosition, owner)
        {
        }

        public override void RecalculateValidMoves()
        {
            // Look right
            for (byte i = 1; ; i++)
                if (!CheckAndInsert(0, i)) break;
            // Look left
            for (byte i = 1; ; i++)
                if (!CheckAndInsert(0, -i)) break;
            // Look up
            for (byte i = 1; ; i++)
                if (!CheckAndInsert(i, 0)) break;
            // Look down
            for (byte i = 1; ; i++)
                if (!CheckAndInsert(-i, 0)) break;
        }

        public override char ChessChar => '♖';
    }
}
