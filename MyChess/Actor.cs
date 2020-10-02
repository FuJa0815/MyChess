using System;

namespace MyChess
{
    public abstract class Actor
    {
        public abstract bool ShowErrors { get; }
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
