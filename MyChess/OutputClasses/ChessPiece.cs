using System;
using System.Collections.Generic;
using System.Linq;

namespace MyChess.OutputClasses
{
    public abstract class ChessPiece : IRender, ICloneable
    {
        public abstract int AiImportance { get; }
        protected ChessPiece(ChessPosition startingPosition, PlayerColor owner)
        {
            CurrentPosition = startingPosition;
            Owner = owner;
        }
        public PlayerColor Owner { get; }
        public ChessPosition CurrentPosition { get; protected set; }
        public List<ChessPosition> ValidMoves { get; protected set; } = new List<ChessPosition>();
        public virtual void RecalculateValidMoves(ChessBoard board)
        {
            ValidMoves.Clear();
        }
        public bool CanMove(ChessPosition target) => ValidMoves.Contains(target);
        internal void Move(ChessPosition target)
        {
            CurrentPosition = target;
        }
        public abstract char ChessChar { get; }
        public void Render()
        {
            Console.ForegroundColor = Owner == PlayerColor.Black ? ConsoleColor.Black : ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition((byte)((CurrentPosition.X - 1) * 4 + 2), (byte)((8 - CurrentPosition.Y) * 2 + 1));
            var chessChar               = Owner == PlayerColor.White ? ChessChar : (char) (ChessChar + 6);
            var line                    = $" {chessChar}";
            if (chessChar != '♟') line += " ";
            Console.Write(line);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        protected bool CheckAndInsert(ChessPosition pos, ChessBoard board)
        {
            if (!ChessBoard.IsInBoard(pos)) return false;
            // TODO: CheckmateCheck
            var b = board[pos];
            if (b == default)
            {
                ValidMoves.Add(pos);
                return true;
            }
            if (b.Owner == Owner) return false;
            ValidMoves.Add(pos);
            return false;
        }
        protected bool CheckAndInsert(int xOff, int yOff, ChessBoard board) =>
            CheckAndInsert(new ChessPosition((byte)(CurrentPosition.X + xOff), (byte)(CurrentPosition.Y + yOff)), board);
        public void Remove(ChessBoard board)
        {
            board.Pieces.Remove(this);
        }

        public abstract object Clone();
    }
}
