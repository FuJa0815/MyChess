using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MyChess
{
    [Serializable]
    public class RoundEndingException : Exception
    {
        public RoundEndingException()
        {
        }

        public RoundEndingException(string message) : base(message)
        {
        }

        public RoundEndingException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RoundEndingException(
            SerializationInfo info,
            StreamingContext  context) : base(info, context)
        {
        }
    }
}
