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
            throw new NotImplementedException();
        }

        public override char ChessChar => '♖';
    }
}
