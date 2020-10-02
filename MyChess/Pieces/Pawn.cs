using System;
using System.Collections.Generic;
using System.Text;
using MyChess.OutputClasses;

namespace MyChess.Pieces
{
    public class Pawn : ChessPiece
    {


        public Pawn(ChessPosition startingPosition, PlayerColor owner) : base(startingPosition, owner)
        {
        }

        public override void RecalculateValidMoves(ChessBoard board)
        {
            base.RecalculateValidMoves(board);
            if ((CurrentPosition.Y == 2 && Owner == PlayerColor.WHITE) ||
               (CurrentPosition.Y == 7 && Owner == PlayerColor.BLACK))
                CheckAndInsertPawnForwards(2, board);
            CheckAndInsertPawnForwards(1, board);
            CheckAndInsertPawnDiagonal(-1, board);
            CheckAndInsertPawnDiagonal(1, board);
        }
        private void CheckAndInsertPawnDiagonal(int xOff, ChessBoard board)
        {
            var pos = new ChessPosition((byte)(CurrentPosition.X + xOff), (byte)(CurrentPosition.Y + (Owner == PlayerColor.WHITE ? 1 : -1)));
            if (!ChessBoard.IsInBoard(pos)) return;
            var b = board[pos];
            if (b == default) return;
            if (!b.Owner.Equals(Owner))
                ValidMoves.Add(pos);
        }
        private void CheckAndInsertPawnForwards(int yOff, ChessBoard board)
        {
            var pos = new ChessPosition(CurrentPosition.X, (byte)(CurrentPosition.Y + (Owner == PlayerColor.WHITE ? yOff : -yOff)));
            if (!ChessBoard.IsInBoard(pos)) return;
            var b = board[pos];
            if (b != default) return;
            ValidMoves.Add(pos);
        }

        public override char ChessChar => '♙';
    }
}
