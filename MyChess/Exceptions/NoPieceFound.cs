using System;
using System.Collections.Generic;
using System.Text;

namespace MyChess.Exceptions
{
    [Serializable]
    public class NoPieceFound : Exception
    {
        private ChessPosition pos;
        public NoPieceFound(ChessPosition pos) : base("No piece at " + pos.ToString()) { this.pos = pos; }
        public NoPieceFound(ChessPosition pos, Exception inner) : base("No piece at " + pos.ToString(), inner) { this.pos = pos; }
        protected NoPieceFound(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
