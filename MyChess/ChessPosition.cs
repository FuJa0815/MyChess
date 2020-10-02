using System;
using System.ComponentModel.DataAnnotations;

namespace MyChess
{
    public struct ChessPosition
    {
        public ChessPosition(byte x, byte y)
        {
            X = x;
            Y = y;
        }
        [Range(1, 8, ErrorMessage = "Out of range")]
        public byte X { get; set; }
        [Range(1, 8, ErrorMessage = "Out of range")]
        public byte Y { get; set; }

        public override string ToString() => ((char)('a' - 1 + X)) + "" + Y;

        public override bool Equals(object obj)
        {
            if (!(obj is ChessPosition cp)) return false;
            return cp.X == X && cp.Y == Y;
        }

        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}
