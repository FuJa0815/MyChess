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
        public abstract ChessMove CalculateMove();

        public override bool Equals(object obj)
        {
            return obj is Actor actor &&
                   Color == actor.Color;
        }

        public override int GetHashCode() => HashCode.Combine(Color);
    }
}
