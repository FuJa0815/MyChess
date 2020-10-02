﻿using System;
using System.Linq;
using MyChess.OutputClasses;
using MyChess.Pieces;

namespace MyChess
{
    public static class RoundManager
    {
        public static Actor CurrentActor { get; private set; } = Program.PlayerW;
        public static uint CurrentRound { get; private set; } = 1;

        internal static void MakeRound()
        {
            var move = CurrentActor.CalculateMove();
            var from = move.From;
            var to = move.To;
            if (!ChessBoard.IsInBoard(from)) throw new Exception(from.ToString() + " is not a valid cell");
            if (!ChessBoard.IsInBoard(to)) throw new Exception(to.ToString() + " is not a valid cell");
            var fromPiece = ChessBoard.CurrentBoard[from];
            var toPiece = ChessBoard.CurrentBoard[to];
            if (fromPiece == default) throw new Exception("No piece at " + from.ToString());
            if (toPiece != default && toPiece.Owner.Equals(CurrentActor)) throw new Exception("Not your piece at "+to.ToString());
            if (!fromPiece.CanMove(to)) throw new Exception("Cannot move from " + from.ToString() + " to " + to.ToString());

            var king = (King)ChessBoard.CurrentBoard.Board.First(p => p is King && p.Owner == CurrentActor.Color);
            if (king.IsCheck() && !(fromPiece is King))
                throw new Exception("Your king is in check!");

            // ValidMove
            if(toPiece != default)
            {
                toPiece.Remove();
            }
            fromPiece.Move(to);
            ChessBoard.CurrentBoard.RecalculateValidMoves();
            if (CurrentActor == Program.PlayerW)
                CurrentActor = Program.PlayerB;
            else
                CurrentActor = Program.PlayerW;
            CurrentRound++;
            ChessBoard.CurrentBoard.Render();
            ChessBoard.CurrentBoard.Board.ForEach(p => p.Render());
        }
    }
}
