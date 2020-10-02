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

        public override void RecalculateValidMoves()
        {
            base.RecalculateValidMoves();
            if ((CurrentPosition.Y == 2 && Owner == PlayerColor.WHITE) ||
               (CurrentPosition.Y == 7 && Owner == PlayerColor.BLACK))
                CheckAndInsertPawnForwards(2);
            CheckAndInsertPawnForwards(1);
            CheckAndInsertPawnDiagonal(-1);
            CheckAndInsertPawnDiagonal(1);
        }
        private void CheckAndInsertPawnDiagonal(int xOff)
        {
            var pos = new ChessPosition((byte)(CurrentPosition.X + xOff), (byte)(CurrentPosition.Y + (Owner == PlayerColor.WHITE ? 1 : -1)));
            if (!ChessBoard.IsInBoard(pos)) return;
            var b = ChessBoard.CurrentBoard[pos];
            if (b == default) return;
            if (!b.Owner.Equals(Owner))
                ValidMoves.Add(pos);
        }
        private void CheckAndInsertPawnForwards(int yOff)
        {
            var pos = new ChessPosition(CurrentPosition.X, (byte)(CurrentPosition.Y + (Owner == PlayerColor.WHITE ? yOff : -yOff)));
            if (!ChessBoard.IsInBoard(pos)) return;
            var b = ChessBoard.CurrentBoard[pos];
            if (b != default) return;
            ValidMoves.Add(pos);
        }

        public override char ChessChar => '♙';
    }
}
