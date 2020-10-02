using MyChess.OutputClasses;

namespace MyChess.Pieces
{
    public class Queen : ChessPiece
    {
        public Queen(ChessPosition startingPosition, PlayerColor owner) : base(startingPosition, owner)
        {
        }

        public override int AiImportance => 9;

        public override void RecalculateValidMoves(ChessBoard board)
        {
            base.RecalculateValidMoves(board);
            // Look right up
            for (byte i = 1; ; i++)
                if (!CheckAndInsert(i, i, board)) break;
            // Look left up
            for (byte i = 1; ; i++)
                if (!CheckAndInsert(-i, i, board)) break;
            // Look right down
            for (byte i = 1; ; i++)
                if (!CheckAndInsert(i, -i, board)) break;
            // Look left down
            for (byte i = 1; ; i++)
                if (!CheckAndInsert(-i, -i, board)) break;

            // Look right
            for (byte i = 1; ; i++)
                if (!CheckAndInsert(0, i, board)) break;
            // Look left
            for (byte i = 1; ; i++)
                if (!CheckAndInsert(0, -i, board)) break;
            // Look up
            for (byte i = 1; ; i++)
                if (!CheckAndInsert(i, 0, board)) break;
            // Look down
            for (byte i = 1; ; i++)
                if (!CheckAndInsert(-i, 0, board)) break;
        }

        public override object Clone() => new Queen(CurrentPosition, Owner) { ValidMoves = this.ValidMoves };

        public override char ChessChar => '♕';
    }
}
