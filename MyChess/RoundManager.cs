using System;
using System.Linq;
using MyChess.OutputClasses;
using MyChess.Pieces;

namespace MyChess
{
    public static class RoundManager
    {
        public static Actor CurrentActor { get; private set; } = Program.PlayerW;
        public static uint CurrentRound { get; private set; } = 1;

        internal static void MakeRound(ChessBoard board)
        {
            var move = CurrentActor.CalculateMove();
            var from = move.From;
            var to = move.To;
            if (!ChessBoard.IsInBoard(from)) throw new Exception(from.ToString() + " is not a valid cell");
            if (!ChessBoard.IsInBoard(to)) throw new Exception(to.ToString() + " is not a valid cell");
            var fromPiece = board[from];
            var toPiece = board[to];
            if (fromPiece == default) throw new Exception("No piece at " + from.ToString());
            if (toPiece != default && toPiece.Owner.Equals(CurrentActor)) throw new Exception("Not your piece at "+to.ToString());
            if (!fromPiece.CanMove(to)) throw new Exception("Cannot move from " + from.ToString() + " to " + to.ToString());

            var king = (King)board.Pieces.First(p => p is King && p.Owner == CurrentActor.Color);
            

            // ValidMove
            if(toPiece != default)
            {
                toPiece.Remove(board);
            }
            fromPiece.Move(to);
            board.RecalculateValidMoves();
            if (king.IsCheck(board) && !(fromPiece is King))
            {
                fromPiece.Move(from);
                board.RecalculateValidMoves();
                throw new Exception("Your king is in check!");
            }

            if (CurrentActor == Program.PlayerW)
                CurrentActor = Program.PlayerB;
            else
                CurrentActor = Program.PlayerW;
            CurrentRound++;
            board.Render();
            board.Pieces.ForEach(p => p.Render());
        }
    }
}
