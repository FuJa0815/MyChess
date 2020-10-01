using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyChess
{
    public abstract class ChessPiece : IRender
    {
        public ChessPiece(ChessPosition startingPosition, PlayerColor owner)
        {
            CurrentPosition = startingPosition;
            Owner = owner;
        }
        public PlayerColor Owner { get; }
        public ChessPosition CurrentPosition { get; protected set; }
        public List<ChessPosition> ValidMoves { get; protected set; } = new List<ChessPosition>();
        public abstract void RecalculateValidMoves();
        public bool CanMove(ChessPosition target) => ValidMoves.Contains(target);
        public void Move(ChessPosition target)
        {
            if (!CanMove(target)) throw new ArgumentException("Invalid target");
            CurrentPosition = target;
        }
        public abstract char ChessChar { get; }
        public void Render()
        {
            Console.ForegroundColor = Owner == PlayerColor.BLACK ? ConsoleColor.Black : ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition((byte)((CurrentPosition.X - 1) * 4 + 2), (byte)((8 - CurrentPosition.Y) * 2 + 1));
            Console.Write($" {(Owner == PlayerColor.WHITE ? ChessChar : (char)(ChessChar + 6))}");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
