using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
    }
}
