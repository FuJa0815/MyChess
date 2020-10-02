using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyChess.OutputClasses;

namespace MyChess.AI
{
    internal static class AIChessBoardHelper
    {
        internal static List<ChessMove> CalculateAllPossibleMoves(ChessBoard board, PlayerColor forPlayer) =>
            board.Pieces
            .Where(p => p.Owner == forPlayer)
            .Select(p => p.ValidMoves.Select(
                x => new ChessMove(p.CurrentPosition, x)))
            .SelectMany(p => p)
            .ToList();
    }
}
