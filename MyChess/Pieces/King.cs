using System;
using System.Collections.Generic;
using System.Text;
using MyChess.OutputClasses;

namespace MyChess.Pieces
{
    public class King : ChessPiece
    {
        public King(ChessPosition startingPosition, PlayerColor owner) : base(startingPosition, owner)
        {
        }

        public override void RecalculateValidMoves(ChessBoard board)
        {
            base.RecalculateValidMoves(board);
            CheckAndInsertKing(-1,-1, board);
            CheckAndInsertKing(-1, 0, board);
            CheckAndInsertKing(-1, 1, board);
            CheckAndInsertKing( 0,-1, board);
            CheckAndInsertKing( 0, 1, board);
            CheckAndInsertKing( 1,-1, board);
            CheckAndInsertKing( 1, 0, board);
        }
        private void CheckAndInsertKing(int xOff, int yOff, ChessBoard board)
        {
            var pos = new ChessPosition((byte)(CurrentPosition.X + xOff), (byte)(CurrentPosition.Y + yOff));
            if(ChessBoard.IsInBoard(pos))
                if (!board.IsProtected(pos, Owner == PlayerColor.WHITE ? PlayerColor.BLACK : PlayerColor.WHITE))
                    CheckAndInsert(pos, board);
        }
        public bool IsCheck(ChessBoard board)
        {
            return board.IsProtected(CurrentPosition, Owner == PlayerColor.WHITE ? PlayerColor.BLACK : PlayerColor.WHITE);
        }

        public override object Clone() => new King(CurrentPosition, Owner);

        public override char ChessChar => '♔';
    }
}
