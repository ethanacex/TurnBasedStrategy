using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Managers;
using StrategyGame.Media;
using System;

namespace StrategyGame.Core
{
    public class MiniMap : Panel
    {
        private RenderTarget2D renderTarget;
        public NavigationFrame NavFrame { get; set; }

        private Point navigationFrameSize;

        public MiniMap(Point location, Point size) : base(location, size)
        {
            SetCustomBorder(3, Color.White);
        }

        public void Initialize(RenderTarget2D renderTarget)
        {
            this.renderTarget = renderTarget;

            // Calculate current screen resolution in ratio to the grid so mini-viewFinder size can be calculated
            var ratioWidth = (double)Settings.ViewFinder.Width / Settings.GridWidth;
            var ratioHeight = (double)Settings.ViewFinder.Height / Settings.GridHeight;

            navigationFrameSize = new Point((int)(Width * ratioWidth), (int)(Height * ratioHeight));
            
            // Resume previous location if there is one stored
            NavFrame = new NavigationFrame(this, GameState.NavigationFramePosition, navigationFrameSize);
        }

        public void UpdatePreview(object source, EventArgs args)
        {
            if (source is GameGrid grid)
                renderTarget = grid.GameWorld;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(renderTarget, Bounds, Color.White);
            sb.End();

            NavFrame.Draw(sb);
            GraphicsManager.DrawGameObjectBorder(sb, this, BorderWidth, borderColor);
        }

        internal void NavigateUp(object sender, EventArgs e)
        {
            NavFrame.NavigateUp(sender, e);
        }

        internal void NavigateDown(object sender, EventArgs e)
        {
            NavFrame.NavigateDown(sender, e);
        }

        internal void NavigateLeft(object sender, EventArgs e)
        {
            NavFrame.NavigateLeft(sender, e);
        }

        internal void NavigateRight(object sender, EventArgs e)
        {
            NavFrame.NavigateRight(sender, e);
        }
    }
}
