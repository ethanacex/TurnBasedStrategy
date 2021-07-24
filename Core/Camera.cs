using Microsoft.Xna.Framework;
using StrategyGame.Managers;

namespace StrategyGame.Core
{
    public static class Camera
    {
        public static Matrix Transform { get; private set; }

        public static void Follow(Point target)
        {
            Transform = Matrix.CreateTranslation(-(target.X), -(target.Y), 0) * Matrix.CreateTranslation(Settings.ViewFinder.Width / 2, Settings.ViewFinder.Height / 2, 0);
        }

    }
}
