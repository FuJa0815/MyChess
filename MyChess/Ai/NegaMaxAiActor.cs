using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MyChess.AI;
using MyChess.OutputClasses;
using MyChess.Pieces;

namespace MyChess.Ai
{
    public class NegaMaxAiActor : AiActor
    {
        public int AiDepth { get; }
        public NegaMaxAiActor(PlayerColor color, int aiDepth) : base(color)
        {
            AiDepth = aiDepth;
        }

        public override ChessMove CalculateMove()
        {
            PrintLoading();
            NegaMax(ChessBoard.CurrentBoard, int.MinValue, int.MaxValue, AiDepth, Color);
            return _result;
        }

        private ChessMove _result;

        private int NegaMax(ChessBoard board, int alpha, int beta, int depth, PlayerColor currPlayer)
        {
            if (depth == 0) return CalcScore(board, currPlayer);
            var totalValue = int.MinValue;
            foreach (var move in board.CalculateAllPossibleMoves(currPlayer))
            {
                var piece  = board[move.From];
                var tPiece = board[move.To];
                if (tPiece is King) return CalcScore(board, currPlayer);
                tPiece?.Remove(board);
                piece.Move(move.To);
                board.RecalculateValidMoves();

                totalValue = -NegaMax(board,  -beta, -alpha, depth - 1, currPlayer == PlayerColor.White ? PlayerColor.Black : PlayerColor.White);

                piece.Move(move.From);
                if (tPiece != default)
                    board.Pieces.Insert(0, tPiece);
                board.RecalculateValidMoves();

                if (totalValue >= alpha)
                {
                    if (depth == AiDepth) _result = move;
                    alpha = totalValue;
                }

                if (alpha >= beta)
                {
                    break;
                }
            }

            return totalValue;
        }

        private int CalcScore(ChessBoard board, PlayerColor col)
        {
            if (((King) board.Pieces.First(p => p.Owner != col && p is King)).CheckCheckmate(board))
                return int.MaxValue;
            if (((King) board.Pieces.First(p => p.Owner == col && p is King)).CheckCheckmate(board))
                return int.MinValue;
            return board.Pieces.Where(p => p.Owner     == col).Sum(p => p.AiImportance) -
                board.Pieces.Where(p => p.Owner != col).Sum(p => p.AiImportance);
        }
    }
}
