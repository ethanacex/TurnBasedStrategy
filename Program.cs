using System;

namespace TurnBasedStrategy
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new StrategyGame())
                game.Run();
        }
    }
}
