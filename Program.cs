using System;

namespace StrategyGame
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
