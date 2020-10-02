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
            if (!ChessBoard.IsInBoard(pos)) return;
            if (board.ProtectedBy(pos, Owner == PlayerColor.White ? PlayerColor.Black : PlayerColor.White).Any())
                CheckAndInsert(pos, board);
        }
        public IEnumerable<ChessPiece> GetChecking(ChessBoard board)
        {
            return board.ProtectedBy(CurrentPosition, Owner == PlayerColor.White ? PlayerColor.Black : PlayerColor.White);
        }

        public bool CheckCheckmate(ChessBoard board)
        {
            board.RecalculateValidMoves();
            var possibleMoves     = board.CalculateAllPossibleMoves(Owner);

            var checking          = GetChecking(board).Select(p=>p.CurrentPosition);
            var checkingPositions = checking as ChessPosition[] ?? checking.ToArray();

            if (ValidMoves.Count > 0) return false;

            if (!checkingPositions.Any())
            {
                if(possibleMoves.Count == 0)
                    throw new RoundEndingException("Remis");
                return false;
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

            throw new RoundEndingException($"{ (Owner == PlayerColor.White ? "Black" : "White") } wins!");
        }

        public override object Clone() => new King(CurrentPosition, Owner) { ValidMoves = this.ValidMoves };

        public override char ChessChar => '♔';
    }
}
