using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyChess.Pieces;

namespace MyChess
{
    public class ChessBoard : IRender
    {
        private static ChessBoard _board;
        public static ChessBoard CBoard
        {
            get
            {
                if (_board != null) return _board;
                _board = new ChessBoard();
                _board.Board.ForEach(p => p.RecalculateValidMoves());
                return _board;
            }
        }
        private ChessBoard()
        {
            Board = new List<ChessPiece>()
            {
                new Rook(new ChessPosition(1,   1), PlayerColor.WHITE),
                new Knight(new ChessPosition(2, 1), PlayerColor.WHITE),
                new Bishop(new ChessPosition(3, 1), PlayerColor.WHITE),
                new Queen(new ChessPosition(4,  1), PlayerColor.WHITE),
                new King(new ChessPosition(5,   1), PlayerColor.WHITE),
                new Bishop(new ChessPosition(6, 1), PlayerColor.WHITE),
                new Knight(new ChessPosition(7, 1), PlayerColor.WHITE),
                new Rook(new ChessPosition(8,   1), PlayerColor.WHITE),
                new Pawn(new ChessPosition(1,   2), PlayerColor.WHITE),
                new Pawn(new ChessPosition(2,   2), PlayerColor.WHITE),
                new Pawn(new ChessPosition(3,   2), PlayerColor.WHITE),
                new Pawn(new ChessPosition(4,   2), PlayerColor.WHITE),
                new Pawn(new ChessPosition(5,   2), PlayerColor.WHITE),
                new Pawn(new ChessPosition(6,   2), PlayerColor.WHITE),
                new Pawn(new ChessPosition(7,   2), PlayerColor.WHITE),
                new Pawn(new ChessPosition(8,   2), PlayerColor.WHITE),

                new Rook(new ChessPosition(1,   8), PlayerColor.BLACK),
                new Knight(new ChessPosition(2, 8), PlayerColor.BLACK),
                new Bishop(new ChessPosition(3, 8), PlayerColor.BLACK),
                new Queen(new ChessPosition(4,  8), PlayerColor.BLACK),
                new King(new ChessPosition(5,   8), PlayerColor.BLACK),
                new Bishop(new ChessPosition(6, 8), PlayerColor.BLACK),
                new Knight(new ChessPosition(7, 8), PlayerColor.BLACK),
                new Rook(new ChessPosition(8,   8), PlayerColor.BLACK),
                new Pawn(new ChessPosition(1,   7), PlayerColor.BLACK),
                new Pawn(new ChessPosition(2,   7), PlayerColor.BLACK),
                new Pawn(new ChessPosition(3,   7), PlayerColor.BLACK),
                new Pawn(new ChessPosition(4,   7), PlayerColor.BLACK),
                new Pawn(new ChessPosition(5,   7), PlayerColor.BLACK),
                new Pawn(new ChessPosition(6,   7), PlayerColor.BLACK),
                new Pawn(new ChessPosition(7,   7), PlayerColor.BLACK),
                new Pawn(new ChessPosition(8,   7), PlayerColor.BLACK),
            };
        }
        public List<ChessPiece> Board { get; }
        public ChessPiece this[ChessPosition c]
        {
            get => Board.FirstOrDefault(p=>p.CurrentPosition.Equals(c));
        }

        private readonly string[] boardText = {
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
        private void RenderLegend()
        {
            Console.SetCursorPosition(0, 1);
            Console.Write("8\n\n7\n\n6\n\n5\n\n4\n\n3\n\n2\n\n1");
            Console.SetCursorPosition(1, 17);
            Console.Write("  a   b   c   d   e   f   g   h");
        }
        private void RenderBoard()
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
        public bool IsInBoard(ChessPosition p) => p.X >= 1 && p.X <= 8 && p.Y >= 1 && p.Y <= 8;
    }
}
