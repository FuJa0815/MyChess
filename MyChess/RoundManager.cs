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
            var clone = (ChessBoard)board.Clone();

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

            var king = (King)clone.Pieces.First(p => p is King && p.Owner == CurrentActor.Color);


            // ValidMove
            toPiece?.Remove(clone);
            fromPiece.Move(to);
            clone.RecalculateValidMoves();
            if (king.IsCheck(clone))
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
