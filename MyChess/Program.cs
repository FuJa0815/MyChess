using System;
using MyChess.Ai;
using MyChess.OutputClasses;

namespace MyChess
{
    internal class Program
    {
        public static Actor PlayerW { get; private set; }
        public static Actor PlayerB { get; private set; }

        private static int AskUserForNumber(string text, int min, int max)
        {
            int? input = null;
            while (input == null)
            {
                Console.WriteLine(text);
                var line = Console.ReadLine();
                int.TryParse(line, out int value);
                if (value < min) continue;
                if (value > max) continue;
                input = value;
            }

            return input.Value;
        }

        private static void Main()
        {
            switch (AskUserForNumber(@"(1) Player vs Player
(2) Player vs AI
(3) AI vs AI", 1, 3))
            {
                case 1:
                    PlayerW = new HumanActor(PlayerColor.White);
                    PlayerB = new HumanActor(PlayerColor.Black);
                    break;
                case 2:
                    PlayerW = new HumanActor(PlayerColor.White);
                    var input = AskUserForNumber("AI depth (0 for random)", 0, 5);
                    if(input == 0)
                        PlayerB = new RandomAiActor(PlayerColor.Black);
                    else
                        PlayerB = new NegaMaxAiActor(PlayerColor.Black, input);
                    break;
                case 3:
                    var input1 = AskUserForNumber("AI 1 depth (0 for random)", 0, 5);
                    var input2 = AskUserForNumber("AI 2 depth (0 for random)",   0, 5);

                    if (input1 == 0)
                        PlayerW = new RandomAiActor(PlayerColor.White);
                    else
                        PlayerW = new NegaMaxAiActor(PlayerColor.White, input1);
                    if (input2 == 0)
                        PlayerB = new RandomAiActor(PlayerColor.Black);
                    else
                        PlayerB = new NegaMaxAiActor(PlayerColor.Black, input2);
                    break;
            }


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
                } catch (RoundEndingException ex)
                {
                    Output.Out.Text = ex.Message;
                    ChessBoard.CurrentBoard.Render();
                    ChessBoard.CurrentBoard.Pieces.ForEach(p=>p.Render());
                    break;
                }
                catch(Exception ex)
                {
                    if(RoundManager.CurrentActor.ShowErrors)
                        Output.Out.Text = ex.Message;
                }
            }
        }
    }
}
