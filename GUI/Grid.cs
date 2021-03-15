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
            Point gridSize = new Point(Settings.GridWidth, Settings.GridHeight);

            Bounds = new Rectangle(Settings.GridPosition, gridSize);
            Tiles = new Tile[Settings.GridColumns, Settings.GridRows];

            Point tilePosition;

            for (int x = 0; x < Settings.GridColumns; x++)
                for (int y = 0; y < Settings.GridRows; y++)
                {
                    tilePosition = new Point(Bounds.X + (x * Settings.TileWidth), Bounds.Y + (y * Settings.TileHeight));
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

        public void ToggleVisibleBorder() => GameState.ToggleGridLines = !GameState.ToggleGridLines;

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
