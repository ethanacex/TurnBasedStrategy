using StrategyGame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using StrategyGame;
using StrategyGame.Media;
using StrategyGame.GUI;

namespace StrategyGame.Screens
{
    class GameScreen : IScreen
    {
        ScreenManager screenManager;
        Panel inspectorPanel;
        Panel statusPanel;
        Panel leftPanel;
        Panel messagePanel;
        Grid grid;

        public void Initialize(ScreenManager screenManager)
        {
            this.screenManager = screenManager;

            grid = new Grid();
            grid.Initialize(Scene.Empty);
            
            inspectorPanel = new Panel(new Point(grid.Bounds.Right, 0), new Point(GraphicsManager.Viewport.Width - grid.Bounds.Width, GraphicsManager.Viewport.Height));
            statusPanel = new Panel(new Point(grid.Bounds.Left, 0), new Point(GraphicsManager.Viewport.Width - inspectorPanel.Bounds.Width, 0 + grid.Bounds.Top));
            leftPanel = new Panel(Point.Zero, new Point(0 + grid.Bounds.Left, GraphicsManager.Viewport.Height));
            messagePanel = new Panel(new Point(leftPanel.Bounds.Right, grid.Bounds.Bottom), new Point(grid.Bounds.Width, GraphicsManager.Viewport.Height - grid.Bounds.Bottom));
        }

        public void Update(GameTime gameTime)
        {
            grid.Update(gameTime);
            inspectorPanel.Update(gameTime);
            statusPanel.Update(gameTime);
            leftPanel.Update(gameTime);
            messagePanel.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            grid.Draw(spriteBatch);
            inspectorPanel.Draw(spriteBatch);
            statusPanel.Draw(spriteBatch);
            leftPanel.Draw(spriteBatch);
            messagePanel.Draw(spriteBatch);
            spriteBatch.End();
        }

    }
}
