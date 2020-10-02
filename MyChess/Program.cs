﻿using System;
using MyChess.AI;
using MyChess.OutputClasses;

namespace MyChess
{
    class Program
    {
        public static Actor PlayerW { get; } = new HumanActor(PlayerColor.WHITE);
        public static Actor PlayerB { get; } = new AIActor(PlayerColor.BLACK);

        static void Main(string[] args)
        {
            ConsoleFontHelper.Init();
            ChessBoard.CurrentBoard.Render();
            ChessBoard.CurrentBoard.Pieces.ForEach(p => p.Render());
            while (true)
            {
                try
                {
                    var b = ChessBoard.CurrentBoard;
                    RoundManager.MakeRound(ref b);
                    Output.Out.Text = "";
                } catch(Exception ex)
                {
                    if(RoundManager.CurrentActor.ShowErrors)
                        Output.Out.Text = ex.Message;
                }
            }
        }
    }
}
