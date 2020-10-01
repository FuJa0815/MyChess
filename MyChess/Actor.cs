using System;
using System.Collections.Generic;
using System.Text;

namespace MyChess
{
    public abstract class Actor
    {
        protected Actor(PlayerColor color)
        {
            Color = color;
        }

        public PlayerColor Color { get; }
        public abstract (ChessPosition from, ChessPosition to) CalculateMove();
    }
}
