namespace MyChess
{
    public readonly struct ChessMove
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
