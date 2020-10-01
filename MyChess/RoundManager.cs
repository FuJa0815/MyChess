using System;
using System.Collections.Generic;
using System.Text;

namespace MyChess
{
    public static class RoundManager
    {
        public static uint CurrRound { get; private set; } = 0;
        public static void ProgressToNextRound()
        {
            CurrRound++;
        }
    }
}
