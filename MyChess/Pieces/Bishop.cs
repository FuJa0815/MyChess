using MyChess.OutputClasses;

namespace MyChess.Pieces
{
    public class Bishop : ChessPiece
    {
        public Bishop(ChessPosition startingPosition, PlayerColor owner) : base(startingPosition, owner)
        {
        }

        public override void RecalculateValidMoves(ChessBoard board)
        {
            base.RecalculateValidMoves(board);
            // Look right up
            for(byte i = 1; ; i++)
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
        }

        public override object Clone() => new Bishop(CurrentPosition, Owner);

        public override char ChessChar => '♗';
    }
}
