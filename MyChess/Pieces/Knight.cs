using System;
using System.Collections.Generic;
using System.Text;

namespace MyChess.Pieces
{
    public class Knight : ChessPiece
    {
        public Knight(ChessPosition startingPosition, PlayerColor owner) : base(startingPosition, owner)
        {
        }

        public override void RecalculateValidMoves()
        {
            base.RecalculateValidMoves();
            CheckAndInsert(-2, -1);
            CheckAndInsert(-2, 1);

            CheckAndInsert(2, -1);
            CheckAndInsert(2, 1);

            CheckAndInsert(-1, 2);
            CheckAndInsert( 1, 2);

            CheckAndInsert(-1, -2);
            CheckAndInsert( 1, -2);
        }

        public override char ChessChar => '♘';
    }
}
