using System;
using System.Collections.Generic;
using System.Text;

namespace MyChess.Exceptions
{
    [Serializable]
    public class NoPieceFound : Exception
    {
        public NoPieceFound(ChessPosition pos) : base(pos.ToString()) { }
        public NoPieceFound(ChessPosition pos, Exception inner) : base(pos.ToString(), inner) { }
        protected NoPieceFound(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
