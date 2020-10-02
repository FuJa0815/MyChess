using System;
using System.Collections.Generic;
using System.Text;

namespace MyChess
{
    public struct ChessMove
    {
        public ChessPosition From { get; }
        public ChessPosition To { get; }
        public ChessMove(ChessPosition from, ChessPosition to)
        {
            From = from;
            To = to;
        }
    }
}
