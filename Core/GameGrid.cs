using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StrategyGame.Managers;

namespace StrategyGame.Core
{
    public class GameGrid : GameObject
    {
        private Tile[,] _tiles;

        public void Initialize(Texture2D levelScene)
        {
            Point gridSize = new Point(Settings.GridWidth, Settings.GridHeight);

            Bounds = new Rectangle(Settings.GridPosition, gridSize);
            _tiles = new Tile[Settings.GridColumns, Settings.GridRows];

            Point tilePosition;
            int counter = 0;
            for (int y = 0; y < Settings.GridRows; y++)
                for (int x = 0; x < Settings.GridColumns; x++)
                {
                    counter++;
                    tilePosition = new Point(Bounds.X + (x * Settings.TileWidth), Bounds.Y + (y * Settings.TileHeight));
                    _tiles[x, y] = new Tile(tilePosition, counter.ToString(), x, y);
                }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var tile in _tiles)
                tile.Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            foreach (var tile in _tiles)
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

    }
}
