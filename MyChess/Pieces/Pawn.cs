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
            if ((CurrentPosition.Y == 2 && Owner == PlayerColor.WHITE) ||
               (CurrentPosition.Y == 7 && Owner == PlayerColor.BLACK))
                CheckAndInsert(0, Owner == PlayerColor.WHITE?2:-2);
            CheckAndInsert(0, Owner == PlayerColor.WHITE ? 1 : -1);
            CheckAndInsertPawn(-1);
            CheckAndInsertPawn(1);
        }
        private void CheckAndInsertPawn(int xOff)
        {
            var pos = new ChessPosition((byte)(CurrentPosition.X + xOff), (byte)(CurrentPosition.Y + Owner == PlayerColor.WHITE ? 1 : -1));
            if (!ChessBoard.CBoard.IsInBoard(pos)) return;
            var b = ChessBoard.CBoard[pos];
            if (b == default) return;
            if (!b.Owner.Equals(Owner))
                ValidMoves.Add(pos);
        }

        public override char ChessChar => '♙';
    }
}
