using System;
using System.Collections.Generic;
using System.Text;
using MyChess.OutputClasses;

namespace MyChess.Pieces
{
    public class Rook : ChessPiece
    {
        public Rook(ChessPosition startingPosition, PlayerColor owner) : base(startingPosition, owner)
        {
        }

        public override void RecalculateValidMoves(ChessBoard board)
        {
            base.RecalculateValidMoves(board);
            // Look right
            for (byte i = 1; ; i++)
                if (!CheckAndInsert(0, i, board)) break;
            // Look left
            for (byte i = 1; ; i++)
                if (!CheckAndInsert(0, -i, board)) break;
            // Look up
            for (byte i = 1; ; i++)
                if (!CheckAndInsert(i, 0, board)) break;
            // Look down
            for (byte i = 1; ; i++)
                if (!CheckAndInsert(-i, 0, board)) break;
        }

        public override object Clone() => new Rook(CurrentPosition, Owner);

        public override char ChessChar => '♖';
    }
}
