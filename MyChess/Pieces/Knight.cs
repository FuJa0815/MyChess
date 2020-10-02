using System;
using System.Collections.Generic;
using System.Text;
using MyChess.OutputClasses;

namespace MyChess.Pieces
{
    public class Knight : ChessPiece
    {
        public Knight(ChessPosition startingPosition, PlayerColor owner) : base(startingPosition, owner)
        {
        }

        public override void RecalculateValidMoves(ChessBoard board)
        {
            base.RecalculateValidMoves(board);
            CheckAndInsert(-2, -1, board);
            CheckAndInsert(-2, 1, board);

            CheckAndInsert(2, -1, board);
            CheckAndInsert(2, 1, board);

            CheckAndInsert(-1, 2, board);
            CheckAndInsert( 1, 2, board);

            CheckAndInsert(-1, -2, board);
            CheckAndInsert( 1, -2, board);
        }

        public override char ChessChar => '♘';
    }
}
