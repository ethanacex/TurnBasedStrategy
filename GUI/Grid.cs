using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TurnBasedStrategy.IO;
using TurnBasedStrategy.Media;
using TurnBasedStrategy.Managers;

namespace TurnBasedStrategy
{
    public class Grid : GameObject
    {
        public Tile[,] Tiles { get; private set; }

        public Grid(int columns, int rows, Texture2D scene)
        {
            Point location;
            Tiles = new Tile[columns, rows];

            for (int x = 0; x < columns; x++)
                for (int y = 0; y < rows; y++)
                {
                    location = new Point(x * Configuration.TileWidth, y * Configuration.TileHeight);
                    Tiles[x, y] = new Tile(location, Scene.Empty);
                }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var tile in Tiles)
                tile.Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            foreach (var tile in Tiles)
                tile.Draw(sb);
        }

        public void ToggleVisibleBorder() => Configuration.ToggleBorder = !Configuration.ToggleBorder;

        private bool MouseOnGrid()
        {
            return Bounds.Contains(Input.CurrentMousePosition) && Bounds.Contains(Input.PreviousMousePosition);
        }

        private bool MouseIntersectsArea(Point point)
        {
            return Bounds.Contains(point);
        }

        private bool MouseIntersectsTileArea()
        {
            return MouseIntersectsArea(Input.CurrentMousePosition) && !MouseIntersectsArea(Input.PreviousMousePosition);
        }

        private bool MouseLeavesTileArea()
        {
            return MouseIntersectsArea(Input.PreviousMousePosition) && !MouseIntersectsArea(Input.CurrentMousePosition);
        }

        private bool MouseHoveringTileArea()
        {
            return MouseIntersectsArea(Input.CurrentMousePosition) && MouseIntersectsArea(Input.PreviousMousePosition);
        }
    }
}
