using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyChess
{
    public abstract class ChessPiece : IRender, IDisposable
    {
        public ChessPiece(ChessPosition startingPosition, PlayerColor owner)
        {
            CurrentPosition = startingPosition;
            Owner = owner;
        }
        public PlayerColor Owner { get; }
        public ChessPosition CurrentPosition { get; protected set; }
        public List<ChessPosition> ValidMoves { get; protected set; } = new List<ChessPosition>();
        public virtual void RecalculateValidMoves()
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
            Console.ForegroundColor = Owner == PlayerColor.BLACK ? ConsoleColor.Black : ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition((byte)((CurrentPosition.X - 1) * 4 + 2), (byte)((8 - CurrentPosition.Y) * 2 + 1));
            Console.Write($" {(Owner == PlayerColor.WHITE ? ChessChar : (char)(ChessChar + 6))}");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        protected bool CheckAndInsert(ChessPosition pos)
        {
            if (!ChessBoard.CBoard.IsInBoard(pos)) return false;
            // TODO: CheckmateCheck
            var b = ChessBoard.CBoard[pos];
            if (b == default)
            {
                ValidMoves.Add(pos);
                return true;
            }
            if (b.Owner == Owner) return false;
            ValidMoves.Add(pos);
            return false;
        }
        protected bool CheckAndInsert(int xOff, int yOff) =>
            CheckAndInsert(new ChessPosition((byte)(CurrentPosition.X + xOff), (byte)(CurrentPosition.Y + yOff)));
        public void Remove()
        {
            Dispose();
            ChessBoard.CBoard.Board.Remove(this);
        }

        ~ChessPiece()
        {
            Dispose();
        }
        public void Dispose()
        {
            
        }
    }
}
