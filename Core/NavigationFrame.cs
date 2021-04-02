using Microsoft.Xna.Framework;
using StrategyGame.Managers;
using StrategyGame.Media;
using System;
using System.Diagnostics;

namespace StrategyGame.Core
{
    public class NavigationFrame : Panel
    {
        private Panel container;

        public NavigationFrame(Panel container, Point location, Point size) : base(location, size)
        {
            this.container = container;
            Texture = Textures.Transparent;
            SetCustomBorder(3, Color.Red);
        }

        public override void Update(GameTime gameTime)
        {

        }

        internal void NavigateUp(object sender, EventArgs e)
        {
            double distancePercent = (double)Settings.TileHeight / Settings.GridHeight;
            int verticalTravelDistance = (int)Math.Floor((double)distancePercent * container.Height) * Settings.NavigationDistance;

            Point verticalPosition = new Point(Bounds.X, Bounds.Y - verticalTravelDistance);

            if (verticalPosition.Y < container.Top)
                verticalPosition.Y = container.Top;
            GraphicsManager.MoveGameObject(this, verticalPosition);
            GameState.NavigationFramePosition = Bounds.Location;
        }

        internal void NavigateDown(object sender, EventArgs e)
        {
            double distancePercent = (double)Settings.TileHeight / Settings.GridHeight;
            var verticalTravelDistance = (int)Math.Floor((double)distancePercent * container.Height) * Settings.NavigationDistance;

            Point verticalPosition = new Point(Bounds.X, Bounds.Y + verticalTravelDistance);
            if (verticalPosition.Y + Bounds.Height > container.Bottom)
                verticalPosition.Y = container.Bottom - Bounds.Height;
            GraphicsManager.MoveGameObject(this, verticalPosition);
            GameState.NavigationFramePosition = Bounds.Location;
        }

        internal void NavigateLeft(object sender, EventArgs e)
        {
            double distancePercent = (double)Settings.TileWidth / Settings.GridWidth;
            int horizontalTravelDistance = (int)Math.Floor((double)distancePercent * container.Width) * Settings.NavigationDistance;

            Point horizontalPosition = new Point(Bounds.X - horizontalTravelDistance, Bounds.Y);
            if (horizontalPosition.X < container.Left)
                horizontalPosition.X = container.Left;
            GraphicsManager.MoveGameObject(this, horizontalPosition);
            GameState.NavigationFramePosition = Bounds.Location;
        }

        internal void NavigateRight(object sender, EventArgs e)
        {
            double distancePercent = (double)Settings.TileWidth / Settings.GridWidth;
            int horizontalTravelDistance = (int)Math.Floor((double)distancePercent * container.Width) * Settings.NavigationDistance;

            Point horizontalPosition = new Point(Bounds.X + horizontalTravelDistance, Bounds.Y);
            if (horizontalPosition.X + Bounds.Width > container.Right)
                horizontalPosition.X = container.Right - Bounds.Width;
            GraphicsManager.MoveGameObject(this, horizontalPosition);
            GameState.NavigationFramePosition = Bounds.Location;
        }
    }
}
