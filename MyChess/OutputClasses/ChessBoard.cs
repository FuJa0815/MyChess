using System;
using System.Collections.Generic;
using System.Linq;
using MyChess.Pieces;

namespace MyChess.OutputClasses
{
    public class ChessBoard : IRender, ICloneable
    {
        public List<ChessMove> CalculateAllPossibleMoves(PlayerColor forPlayer) =>
            Pieces
            .Where(p => p.Owner == forPlayer)
            .Select(p => p.ValidMoves.Select(
                                             x => new ChessMove(p.CurrentPosition, x)))
            .SelectMany(p => p)
            .ToList();
        public IEnumerable<ChessPiece> ProtectedBy(ChessPosition pos, PlayerColor by)
        {
            foreach (var enemy in Pieces.Where(p => p.Owner == by))
            {
                if (enemy is Pawn p)
                {
                    ChessPosition pos1;
                    ChessPosition pos2;
                    if (p.Owner == PlayerColor.White)
                    {
                        // Move up
                        pos1 = new ChessPosition((byte)(p.CurrentPosition.X - 1), (byte)(p.CurrentPosition.Y + 1));
                        pos2 = new ChessPosition((byte)(p.CurrentPosition.X + 1), (byte)(p.CurrentPosition.Y + 1));

                    }
                    else
                    {
                        // Move down
                        pos1 = new ChessPosition((byte)(p.CurrentPosition.X - 1), (byte)(p.CurrentPosition.Y - 1));
                        pos2 = new ChessPosition((byte)(p.CurrentPosition.X + 1), (byte)(p.CurrentPosition.Y - 1));
                    }
                    if (pos1.Equals(pos) || pos2.Equals(pos))
                    {
                        yield return enemy;
                    }
                }
                else if (enemy.ValidMoves.Contains(pos))
                    yield return enemy;
            }
        }

        private static ChessBoard _currentBoard;
        public static ChessBoard CurrentBoard
        {
            get
            {
                if (_currentBoard != null) return _currentBoard;
                _currentBoard = CreateStandardChessBoard();
                _currentBoard.RecalculateValidMoves();
                return _currentBoard;
            }
        }
        private ChessBoard() { }
        private static ChessBoard CreateStandardChessBoard()
        {
            return new ChessBoard()
            {
                Pieces = new List<ChessPiece>()
                {
                    new Rook(new ChessPosition(1,   1), PlayerColor.White),
                    new Knight(new ChessPosition(2, 1), PlayerColor.White),
                    new Bishop(new ChessPosition(3, 1), PlayerColor.White),
                    new Queen(new ChessPosition(4,  1), PlayerColor.White),
                    new Bishop(new ChessPosition(6, 1), PlayerColor.White),
                    new Knight(new ChessPosition(7, 1), PlayerColor.White),
                    new Rook(new ChessPosition(8,   1), PlayerColor.White),
                    new Pawn(new ChessPosition(1,   2), PlayerColor.White),
                    new Pawn(new ChessPosition(2,   2), PlayerColor.White),
                    new Pawn(new ChessPosition(3,   2), PlayerColor.White),
                    new Pawn(new ChessPosition(4,   2), PlayerColor.White),
                    new Pawn(new ChessPosition(5,   2), PlayerColor.White),
                    new Pawn(new ChessPosition(6,   2), PlayerColor.White),
                    new Pawn(new ChessPosition(7,   2), PlayerColor.White),
                    new Pawn(new ChessPosition(8,   2), PlayerColor.White),

                    new Rook(new ChessPosition(1,   8), PlayerColor.Black),
                    new Knight(new ChessPosition(2, 8), PlayerColor.Black),
                    new Bishop(new ChessPosition(3, 8), PlayerColor.Black),
                    new Queen(new ChessPosition(4,  8), PlayerColor.Black),
                    new Bishop(new ChessPosition(6, 8), PlayerColor.Black),
                    new Knight(new ChessPosition(7, 8), PlayerColor.Black),
                    new Rook(new ChessPosition(8,   8), PlayerColor.Black),
                    new Pawn(new ChessPosition(1,   7), PlayerColor.Black),
                    new Pawn(new ChessPosition(2,   7), PlayerColor.Black),
                    new Pawn(new ChessPosition(3,   7), PlayerColor.Black),
                    new Pawn(new ChessPosition(4,   7), PlayerColor.Black),
                    new Pawn(new ChessPosition(5,   7), PlayerColor.Black),
                    new Pawn(new ChessPosition(6,   7), PlayerColor.Black),
                    new Pawn(new ChessPosition(7,   7), PlayerColor.Black),
                    new Pawn(new ChessPosition(8,   7), PlayerColor.Black),

                    new King(new ChessPosition(5, 8), PlayerColor.Black),
                    new King(new ChessPosition(5, 1), PlayerColor.White),
                }
            };
        }
        public void RecalculateValidMoves() =>
            Pieces.AsParallel().ForAll(p => p.RecalculateValidMoves(this));
        public List<ChessPiece> Pieces { get; internal set; }
        public ChessPiece this[ChessPosition c] => Pieces.FirstOrDefault(p => p.CurrentPosition.Equals(c));

        private static readonly string[] boardText = {
            "+-------------------------------+",
            "|   |   |   |   |   |   |   |   |",
            "|---+---+---+---+---+---+---+---|",
            "|   |   |   |   |   |   |   |   |",
            "|---+---+---+---+---+---+---+---|",
            "|   |   |   |   |   |   |   |   |",
            "|---+---+---+---+---+---+---+---|",
            "|   |   |   |   |   |   |   |   |",
            "|---+---+---+---+---+---+---+---|",
            "|   |   |   |   |   |   |   |   |",
            "|---+---+---+---+---+---+---+---|",
            "|   |   |   |   |   |   |   |   |",
            "|---+---+---+---+---+---+---+---|",
            "|   |   |   |   |   |   |   |   |",
            "|---+---+---+---+---+---+---+---|",
            "|   |   |   |   |   |   |   |   |",
            "+-------------------------------+"
        };
        private static void RenderLegend()
        {
            Console.SetCursorPosition(0, 1);
            Console.Write("8\n\n7\n\n6\n\n5\n\n4\n\n3\n\n2\n\n1");
            Console.SetCursorPosition(1, 17);
            Console.Write("  a   b   c   d   e   f   g   h");
        }
        private static void RenderBoard()
        {
            for (var i = 0; i < boardText.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(1, i);
                Console.Write(boardText[i]);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void Render()
        {
            RenderLegend();
            RenderBoard();
        }

        public static bool IsInBoard(ChessPosition p) => p.X >= 1 && p.X <= 8 && p.Y >= 1 && p.Y <= 8;

        public object Clone()
        {
            var temp = new ChessBoard {Pieces = Pieces.Select(p => (ChessPiece) p.Clone()).ToList()};
            temp.RecalculateValidMoves();
            return temp;
        }
    }
}
