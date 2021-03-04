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

        internal static void MakeRound(ref ChessBoard board)
        {
            board.RecalculateValidMoves();
            var clone       = (ChessBoard)board.Clone();
            var king = (King)clone.Pieces.First(p => p is King && p.Owner == CurrentActor.Color);
            var kingInCheck = king.GetChecking(clone, king.CurrentPosition).Any();
            var possibleMoves = board.CalculateAllPossibleMoves(CurrentActor.Color);
            if (!possibleMoves.Any() && !kingInCheck)
                throw new RoundEndingException("Remis");
            if (king.CheckCheckmate(board, possibleMoves))
                throw new RoundEndingException($"{king.Owner} is checkmate!");
            var move = CurrentActor.CalculateMove();
            var from = move.From;
            var to = move.To;
            if (!ChessBoard.IsInBoard(from)) throw new Exception(@from + " is not a valid cell");
            if (!ChessBoard.IsInBoard(to)) throw new Exception(to + " is not a valid cell");
            var fromPiece = clone[from];
            var toPiece = clone[to];
            if (fromPiece == default) throw new Exception("No piece at " + @from);
            if (toPiece != default && toPiece.Owner.Equals(CurrentActor.Color)) throw new Exception("Not your piece at "+to);
            if (!fromPiece.CanMove(to)) throw new Exception("Cannot move from " + @from + " to " + to);

            


            // ValidMove
            toPiece?.Remove(clone);
            fromPiece.Move(to, clone);
            if (fromPiece is Pawn pawn)
                fromPiece = pawn.TryUpgrade(clone) ?? fromPiece;
            CurrentActor = Equals(CurrentActor, Program.PlayerW) ? Program.PlayerB : Program.PlayerW;
            clone.RecalculateValidMoves();
            CurrentActor = Equals(CurrentActor, Program.PlayerW) ? Program.PlayerB : Program.PlayerW;
            king        = (King)clone.Pieces.First(p => p is King && p.Owner == CurrentActor.Color);
            kingInCheck = king.GetChecking(clone, king.CurrentPosition).Any();
            
            if (kingInCheck)
            {
                throw new Exception("Your king is in check!");
            }
            board.Pieces = clone.Pieces;
            CurrentActor = Equals(CurrentActor, Program.PlayerW) ? Program.PlayerB : Program.PlayerW;
            CurrentRound++;
            board.Render();
            board.Pieces.ForEach(p => p.Render());
        }
    }
}
