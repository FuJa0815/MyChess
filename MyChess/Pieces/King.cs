﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyChess.Pieces
{
    public class King : ChessPiece
    {
        public King(ChessPosition startingPosition, PlayerColor owner) : base(startingPosition, owner)
        {
        }

        public override void RecalculateValidMoves()
        {
            throw new NotImplementedException();
        }

        public override char ChessChar => '♔';
    }
}
