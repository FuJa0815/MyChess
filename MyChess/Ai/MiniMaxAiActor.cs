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
    public class MiniMaxAiActor : AiActor
    {
        public int AiDepth { get; }
        public MiniMaxAiActor(PlayerColor color, int aiDepth) : base(color)
        {
            AiDepth = aiDepth;
        }

        public override ChessMove CalculateMove()
        {
            PrintLoading();
            max(Color, AiDepth, int.MinValue, int.MaxValue);
            ChessBoard.CurrentBoard.RecalculateValidMoves();
            return _result;
        }

        private ChessMove _result;

        private int max(PlayerColor player, int depth, int alpha, int beta)
        {
            if (depth == 0) return CalcScore(ChessBoard.CurrentBoard);
            int maxValue = alpha;
            ChessBoard.CurrentBoard.RecalculateValidMoves();
            var moves    = ChessBoard.CurrentBoard.CalculateAllPossibleMoves(player);
            foreach (var move in moves)
            {
                var movingPiece = ChessBoard.CurrentBoard[move.From];
                var targetPiece = ChessBoard.CurrentBoard[move.To];
                targetPiece?.Remove(ChessBoard.CurrentBoard);
                movingPiece.Move(move.To, ChessBoard.CurrentBoard);
                Pawn upgradedPawn = null;
                if (movingPiece is Pawn pawn)
                {
                    var tmp = pawn.TryUpgrade(ChessBoard.CurrentBoard);
                    if (tmp != null)
                    {
                        movingPiece = tmp;
                        upgradedPawn = pawn;
                    }
                }

                var value       = min(player == PlayerColor.White ? PlayerColor.Black : PlayerColor.White, depth - 1, maxValue, beta);
                
                if(targetPiece != default)
                    ChessBoard.CurrentBoard.Pieces.Add(targetPiece);
                if (upgradedPawn != null)
                {
                    movingPiece.Remove(ChessBoard.CurrentBoard);
                    ChessBoard.CurrentBoard.Pieces.Add(upgradedPawn);
                    movingPiece = upgradedPawn;
                }
                movingPiece.Move(move.From, ChessBoard.CurrentBoard);

                if (value > maxValue)
                {
                    maxValue = value;
                    if (depth == AiDepth)
                    {
                        _result = move;
                    }

                    if (maxValue >= beta) break;
                }
            }

            return maxValue;
        }

        private int min(PlayerColor player, int depth, int alpha, int beta)
        {
            if (depth == 0) return CalcScore(ChessBoard.CurrentBoard);
            int minValue = beta;
            ChessBoard.CurrentBoard.RecalculateValidMoves();
            var moves    = ChessBoard.CurrentBoard.CalculateAllPossibleMoves(player);
            foreach (var move in moves)
            {
                var movingPiece = ChessBoard.CurrentBoard[move.From];
                var targetPiece = ChessBoard.CurrentBoard[move.To];
                targetPiece?.Remove(ChessBoard.CurrentBoard);
                movingPiece.Move(move.To, ChessBoard.CurrentBoard);
                Pawn upgradedPawn = null;
                if (movingPiece is Pawn pawn)
                {
                    var tmp = pawn.TryUpgrade(ChessBoard.CurrentBoard);
                    if (tmp != null)
                    {
                        movingPiece = tmp;
                        upgradedPawn = pawn;
                    }
                }


                var value = max(player == PlayerColor.White ? PlayerColor.Black : PlayerColor.White, depth - 1, alpha, minValue);
                
                if (targetPiece != default)
                    ChessBoard.CurrentBoard.Pieces.Add(targetPiece);
                if(upgradedPawn != null)
                {
                    movingPiece.Remove(ChessBoard.CurrentBoard);
                    ChessBoard.CurrentBoard.Pieces.Add(upgradedPawn);
                    movingPiece = upgradedPawn;
                }
                movingPiece.Move(move.From, ChessBoard.CurrentBoard);

                if (value < minValue)
                {
                    minValue = value;
                    if (minValue <= alpha) break;
                }
            }

            return minValue;
        }
        /*private int NegaMax(ChessBoard board, int alpha, int beta, int depth, PlayerColor currPlayer)
        {
            if (depth == 0) return CalcScore(board, currPlayer);
            var totalValue = int.MinValue;
            foreach (var move in board.CalculateAllPossibleMoves(currPlayer))
            {
                board.RecalculateValidMoves();
                var piece  = board[move.From];
                var tPiece = board[move.To];
                if (tPiece is King) return CalcScore(board, currPlayer);
                tPiece?.Remove(board);
                piece.Move(move.To);

                totalValue = -NegaMax(board,  -beta, -alpha, depth - 1, currPlayer == PlayerColor.White ? PlayerColor.Black : PlayerColor.White);

                if(tPiece != default)
                    board.Pieces.Add(tPiece);
                piece.Move(move.From);

                if (Color == PlayerColor.Black)
                {
                    if (totalValue >= alpha)
                    {
                        if (depth == AiDepth) _result = move;
                        alpha = totalValue;
                    }
                } else
                {
                    if (totalValue <= alpha)
                    {
                        if (depth == AiDepth) _result = move;
                        alpha = totalValue;
                    }
                }

                if (Color == PlayerColor.White)
                {
                    if (alpha >= beta)
                    {
                        break;
                    }
                } else
                {
                    if (alpha <= beta)
                    {
                        break;
                    }
                }
            }

            return totalValue;
        }*/

        private int CalcScore(ChessBoard board)
        {
            /*var wKing = (King) board.Pieces.First(p => p.Owner == PlayerColor.White && p is King);
            var bKing = (King)board.Pieces.First(p => p.Owner  == PlayerColor.Black && p is King);
            if (wKing.CheckCheckmate(board)) return int.MinValue;
            if (bKing.CheckCheckmate(board)) return int.MaxValue;*/
            if (Color == PlayerColor.Black)
            {
                return board.Pieces.Where(p => p.Owner == PlayerColor.Black).Sum(p => p.AiImportance) -
                       board.Pieces.Where(p => p.Owner == PlayerColor.White).Sum(p => p.AiImportance);
            } else
            {
                return board.Pieces.Where(p => p.Owner == PlayerColor.White).Sum(p => p.AiImportance) -
                       board.Pieces.Where(p => p.Owner == PlayerColor.Black).Sum(p => p.AiImportance);
            }

        }
    }
}
