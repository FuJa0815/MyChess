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

        public override void RecalculateValidMoves()
        {
            base.RecalculateValidMoves();
            CheckAndInsertKing(-1,-1);
            CheckAndInsertKing(-1, 0);
            CheckAndInsertKing(-1, 1);
            CheckAndInsertKing( 0,-1);
            CheckAndInsertKing( 0, 1);
            CheckAndInsertKing( 1,-1);
            CheckAndInsertKing( 1, 0);
        }
        private void CheckAndInsertKing(int xOff, int yOff)
        {
            var pos = new ChessPosition((byte)(CurrentPosition.X + xOff), (byte)(CurrentPosition.Y + yOff));
            if (!ChessBoard.CurrentBoard.IsProtected(pos, Owner == PlayerColor.WHITE ? PlayerColor.BLACK : PlayerColor.WHITE))
                CheckAndInsert(pos);
        }
        public override char ChessChar => '♔';
    }
}
