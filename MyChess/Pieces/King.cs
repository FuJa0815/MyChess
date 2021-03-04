using System;
using System.Collections.Generic;
using System.Linq;
using MyChess.OutputClasses;

namespace MyChess.Pieces
{
    public class King : ChessPiece
    {
        public King(ChessPosition startingPosition, PlayerColor owner) : base(startingPosition, owner)
        {
        }

        public override int AiImportance => 900;

        public override void RecalculateValidMoves(ChessBoard board)
        {
            base.RecalculateValidMoves(board);
            CheckAndInsertKing(-1, -1, board);
            CheckAndInsertKing(-1, 0,  board);
            CheckAndInsertKing(-1, 1,  board);
            CheckAndInsertKing( 0, -1, board);
            CheckAndInsertKing( 0, 1,  board);
            CheckAndInsertKing( 1, -1, board);
            CheckAndInsertKing( 1, 0,  board);
            CheckAndInsertKing(1,  1,  board);
        }
        private void CheckAndInsertKing(int xOff, int yOff, ChessBoard board)
        {
            var pos = new ChessPosition((byte)(CurrentPosition.X + xOff), (byte)(CurrentPosition.Y + yOff));
            if (!ChessBoard.IsInBoard(pos)) return;
            if (!GetChecking(board, pos).Any())
                CheckAndInsert(pos, board);
        }
        public IEnumerable<ChessPiece> GetChecking(ChessBoard board, ChessPosition pos)
        {
            return board.ProtectedBy(pos, Owner == PlayerColor.White ? PlayerColor.Black : PlayerColor.White);
        }

        public bool CheckCheckmate(ChessBoard board, List<ChessMove> possibleMoves)
        {
            var checking          = GetChecking(board, CurrentPosition).Select(p=>p.CurrentPosition);
            var checkingPositions = checking as ChessPosition[] ?? checking.ToArray();

            // Nobody is checking me
            if (!checkingPositions.Any())
            {
                if (possibleMoves.Count > 0) return false;
                throw new RoundEndingException("Remis");
            }

            foreach (var move in possibleMoves)
            {
                var clone  = (ChessBoard)board.Clone();
                var piece  = clone[move.From];
                var target = clone[move.To];
                target?.Remove(clone);
                piece.Move(move.To);
                var resolved = true;
                foreach (var ch in checkingPositions)
                {
                    var p = clone[ch];
                    if(p == default) continue;
                    p.RecalculateValidMoves(clone);
                    if (!p.ValidMoves.Contains(CurrentPosition)) continue;
                    resolved = false;
                    break;
                }
                if (resolved) return false;
            }

            return true;
        }

        public override object Clone() => new King(CurrentPosition, Owner) { ValidMoves = this.ValidMoves };

        public override char ChessChar => '♔';
    }
}
