using Microsoft.Xna.Framework;

using StrategyGame.Managers;

namespace StrategyGame.Core
{
    public class ViewFinder : Panel
    {
        public ViewFinder(Point location, Point size) : base(location, size) { }

        public void SetMainView()
        {
            // Update ViewFinder in Settings so that other classes can query
            Settings.ViewFinder = this;

            // For now, we will render grid in top left of view. Later, Grid should be positioned center-bottom of screen
            Settings.GridPosition = Bounds.Location;

            //Settings.GridPosition = new Point(0 - (Settings.GridWidth / 2), (0 - Settings.GridHeight) + Bounds.Height);

        }

    }
}
