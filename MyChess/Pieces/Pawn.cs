using System;
using System.Collections.Generic;
using System.Text;

namespace MyChess.Pieces
{
    public class Pawn : ChessPiece
    {
        public Pawn(ChessPosition startingPosition, PlayerColor owner) : base(startingPosition, owner)
        {
        }

        public override void RecalculateValidMoves()
        {
            throw new NotImplementedException();
        }

        public override char ChessChar => '♙';
    }
}
