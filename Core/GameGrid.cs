using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StrategyGame.Managers;
using StrategyGame.Media;
using System;

namespace StrategyGame.Core
{
    public class GameGrid : GameObject
    {
        private GridTile[,] tiles;
        private Point gridSize;

        public RenderTarget2D GameWorld;

        public event EventHandler<EventArgs> MapUpdated;

        public void OnMapUpdated()
        {
            MapUpdated?.Invoke(this, EventArgs.Empty);
        }

        private void DrawSceneToRenderTarget()
        {
            GraphicsManager.GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsManager.GraphicsDevice.SetRenderTargets(GameWorld);

            SpriteBatch sb = new SpriteBatch(GraphicsManager.GraphicsDevice);

            sb.Begin();
            sb.Draw(Texture, Bounds, Color.White);

            // DEBUGGING - Draw random colored blobs throughout the map to see if they appear on the minimap
            sb.Draw(Textures.TestRed, new Rectangle(450, 600, 50, 50), Color.White);
            sb.Draw(Textures.TestPink, new Rectangle(1250, 1000, 50, 50), Color.White);

            sb.End();

            GraphicsManager.GraphicsDevice.SetRenderTarget(null);
        }

        private void DrawGridLines(SpriteBatch sb)
        {
            var lineTexture = GraphicsManager.GetTextureOfColor(Color.Black);
            var lineWidth = 1;

            sb.Begin();

            for (int x = Bounds.Left; x < Bounds.Right; x += Settings.TileWidth)
                sb.Draw(lineTexture, new Rectangle(x, Bounds.Top, lineWidth, Bounds.Height), Color.White);

            for (int y = Bounds.Top; y < Bounds.Bottom; y += Settings.TileHeight)
                sb.Draw(lineTexture, new Rectangle(Bounds.Left, y, Bounds.Width, lineWidth), Color.White);

            sb.End();
        }

        private void DrawTileData(SpriteBatch sb)
        {
            int counter = 0;

            sb.Begin();

            for (int y = Bounds.Top; y < Bounds.Bottom; y += Settings.TileHeight)
            {
                for (int x = Bounds.Left; x < Bounds.Right; x += Settings.TileWidth)
                {
                    counter++;
                    sb.DrawString(Fonts.Tile, counter.ToString(), new Vector2(x, y), Color.Black);
                }
            }

            sb.End();
        }

        public void Initialize(Texture2D levelScene)
        {
            Texture = levelScene;
            gridSize = new Point(Settings.GridWidth, Settings.GridHeight);
            Bounds = new Rectangle(Point.Zero, gridSize);

            GameWorld = new RenderTarget2D(GraphicsManager.GraphicsDevice, Bounds.Width, Bounds.Height);

            DrawSceneToRenderTarget();

            tiles = new GridTile[Settings.GridColumns, Settings.GridRows];

            InitTiles();

        }

        private void InitTiles()
        {
            Point tilePosition;
            int counter = 0;
            for (int y = 0; y < Settings.GridRows; y++)
                for (int x = 0; x < Settings.GridColumns; x++)
                {
                    counter++;
                    tilePosition = new Point(Bounds.X + (x * Settings.TileWidth), Bounds.Y + (y * Settings.TileHeight));
                    tiles[x, y] = new GridTile(tilePosition, counter.ToString(), x, y);
                }
        }

        public override void Update(GameTime gameTime)
        {
            if (Settings.GridPosition != Bounds.Location)
                Bounds = new Rectangle(Settings.GridPosition, gridSize);
            if (Bounds.Contains(Input.CurrentMousePosition))
                foreach (var tile in tiles)
                    tile.Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(GameWorld, Bounds, Color.White);
            sb.End();

            DrawGridLines(sb);
            DrawTileData(sb);

            foreach (var tile in tiles)
                tile.Draw(sb);
        }

    }
}
