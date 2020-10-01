using System;

namespace MyChess
{
    public static class RoundManager
    {
        public static Actor CurrentActor { get; private set; } = Program.PlayerW;
        public static uint CurrentRound { get; private set; } = 1;

        internal static void MakeRound()
        {
            var move = CurrentActor.CalculateMove();
            var from = move.from;
            var to = move.to;
            if (!ChessBoard.CBoard.IsInBoard(from)) throw new Exception(from.ToString() + " is not a valid cell");
            if (!ChessBoard.CBoard.IsInBoard(to)) throw new Exception(to.ToString() + " is not a valid cell");
            var fromPiece = ChessBoard.CBoard[from];
            var toPiece = ChessBoard.CBoard[to];
            if (fromPiece == default) throw new Exception("No piece at " + from.ToString());
            if (toPiece != default && toPiece.Owner.Equals(CurrentActor)) throw new Exception("Not your piece at "+to.ToString());
            if (!fromPiece.CanMove(to)) throw new Exception("Cannot move from " + from.ToString() + " to " + to.ToString());


            // ValidMove
            if(toPiece != default)
            {
                toPiece.Remove();
            }
            fromPiece.Move(to);
            ChessBoard.CBoard.RecalculateValidMoves();
            if (CurrentActor == Program.PlayerW)
                CurrentActor = Program.PlayerB;
            else
                CurrentActor = Program.PlayerW;
            CurrentRound++;
            ChessBoard.CBoard.Render();
            ChessBoard.CBoard.Board.ForEach(p => p.Render());
        }
    }
}
