using System;
using System.Collections.Generic;
using System.Text;

namespace MyChess.Pieces
{
    public class King : ChessPiece
    {
        public King(ChessPosition startingPosition, PlayerColor owner) : base(startingPosition, owner)
        {
        }

        public override void RecalculateValidMoves()
        {
            CheckAndInsert(-1,-1);
            CheckAndInsert(-1, 0);
            CheckAndInsert(-1, 1);
            CheckAndInsert( 0,-1);
            CheckAndInsert( 0, 1);
            CheckAndInsert( 1,-1);
            CheckAndInsert( 1, 0);
            CheckAndInsert( 1, 1);
        }

        public override char ChessChar => '♔';
    }
}
