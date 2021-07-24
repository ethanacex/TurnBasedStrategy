using Microsoft.Xna.Framework;

using StrategyGame.Managers;

namespace StrategyGame.Core
{
    public class ViewFinder : Panel
    {
        public ViewFinder(Point location, Point size) : base(location, size) { }

        public void SetMainView()
        {
            Settings.ViewFinder = this;

            Settings.GridPosition = Bounds.Location;
        }

    }
}
