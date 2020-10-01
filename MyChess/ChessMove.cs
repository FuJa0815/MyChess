using System;
using System.Collections.Generic;
using System.Text;

namespace MyChess
{
    public struct ChessMove
    {
        public ChessMove(ChessPiece piece, ChessPosition target)
        {
            Piece = piece;
            Target = target;
        }

        public ChessPiece Piece { get; }
        public ChessPosition Target { get; }
    }
}
