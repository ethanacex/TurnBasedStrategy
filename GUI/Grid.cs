using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using StrategyGame.IO;
using StrategyGame.Media;
using StrategyGame.Managers;

namespace StrategyGame.GUI
{
    public class Grid : GameObject
    {
        public Tile[,] Tiles { get; private set; }

        public void Initialize(Texture2D levelScene)
        {
            Point gridPosition = new Point(50, 50);
            Point gridSize = new Point(Configuration.GridWidth, Configuration.GridHeight);

            Bounds = new Rectangle(gridPosition, gridSize);
            Tiles = new Tile[Configuration.GridColumns, Configuration.GridRows];

            Point tilePosition;

            for (int x = 0; x < Configuration.GridColumns; x++)
                for (int y = 0; y < Configuration.GridRows; y++)
                {
                    tilePosition = new Point(Bounds.X + (x * Configuration.TileWidth), Bounds.Y + (y * Configuration.TileHeight));
                    Tiles[x, y] = new Tile(tilePosition);
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

        public void ToggleVisibleBorder() => GameState.ToggleBorder = !GameState.ToggleBorder;

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
