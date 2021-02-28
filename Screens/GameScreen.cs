using StrategyGame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Media;
using StrategyGame.GUI;
using StrategyGame.IO;

namespace StrategyGame.Screens
{
    class GameScreen : IScreen
    {
        ScreenManager screenManager;

        Panel inspectorPanel;
        Panel inspectorWindow;
        Panel statusPanel;
        Panel leftPanel;
        Panel rightPanel;
        Panel messagePanel;

        Label goldTextLabel;
        Label goldValueLabel;
        Label goldProductionLabel;
        Label unitsTextLabel;
        Label unitsValueLabel;
        Label inspectorLabel;
        Label selectedTileLabel;
        Label tileStatusLabel;
        Label tileInfoLabel;

        Grid gameGrid;

        public void Initialize(ScreenManager screenManager)
        {
            this.screenManager = screenManager;
            Viewport screen = GraphicsManager.Viewport;

            // Get the rendered font height so we can position the grid below it
            Point labelDimensions = GraphicsManager.GetLabelDimensions("Units: 10");
            Point gridPosition = Settings.GridPosition;
            gridPosition.Y = labelDimensions.Y;
            Settings.GridPosition = gridPosition;

            // Initialize Grid
            gameGrid = new Grid();
            gameGrid.Initialize(Textures.Empty);

            // Initialize Panels
            Rectangle grid = gameGrid.Bounds;
            leftPanel = new Panel(Point.Zero, new Point(grid.Left, screen.Height));
            messagePanel = new Panel(new Point(grid.Left, grid.Bottom), new Point(grid.Width, screen.Height - grid.Bottom));
            statusPanel = new Panel(new Point(grid.Left, 0), new Point(grid.Width, grid.Top));
            rightPanel = new Panel(new Point(grid.Right, 0), new Point(15, screen.Height));
            inspectorPanel = new Panel(new Point(rightPanel.Bounds.Right, 0), new Point(screen.Width - (grid.Width + leftPanel.Bounds.Width + rightPanel.Bounds.Width), screen.Height));
            inspectorWindow = new Panel(new Point(inspectorPanel.Bounds.X, gridPosition.Y), new Point(350, 300));
            inspectorWindow.SetBorder(3, Color.White);

            // Initialize Status Bar
            Rectangle currentPanel = statusPanel.Bounds;
            goldTextLabel = new Label("Gold: ", currentPanel.Location.ToVector2(), Fonts.InGameFont, Color.Cyan);
            goldValueLabel = new Label("120", new Vector2(goldTextLabel.Bounds.Right, currentPanel.Y), Fonts.InGameFont, Color.White);
            goldProductionLabel = new Label(" (+75)", new Vector2(goldValueLabel.Bounds.Right, currentPanel.Y), Fonts.InGameFont, Color.Yellow);
            unitsTextLabel = new Label("Units: ", new Vector2(statusPanel.Bounds.Right-labelDimensions.X, currentPanel.Y), Fonts.InGameFont, Color.Cyan);
            unitsValueLabel = new Label("04", new Vector2(unitsTextLabel.Bounds.Right, currentPanel.Y), Fonts.InGameFont, Color.White);

            // Initialize Inspector Panel
            currentPanel = inspectorPanel.Bounds;
            inspectorLabel = new Label("Inspector", currentPanel.Location.ToVector2(), Fonts.InGameFont, Color.Cyan);
            selectedTileLabel = new Label("Selected Tile: ", new Vector2(currentPanel.X, inspectorWindow.Bounds.Bottom + inspectorWindow.BorderWidth + 20), Fonts.InGameFont, Color.Cyan);
            tileStatusLabel = new Label("Status: ", new Vector2(currentPanel.X, selectedTileLabel.Bounds.Bottom), Fonts.InGameFont, Color.Cyan);
            tileInfoLabel = new Label("Output: ", new Vector2(currentPanel.X, tileStatusLabel.Bounds.Bottom), Fonts.InGameFont, Color.Cyan);
        }

        public void Update(GameTime gameTime)
        {
            gameGrid.Update(gameTime);
            inspectorPanel.Update(gameTime);
            statusPanel.Update(gameTime);
            leftPanel.Update(gameTime);
            messagePanel.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            gameGrid.Draw(spriteBatch);

            leftPanel.Draw(spriteBatch);
            messagePanel.Draw(spriteBatch);
            statusPanel.Draw(spriteBatch);
            rightPanel.Draw(spriteBatch);
            inspectorPanel.Draw(spriteBatch);
            inspectorWindow.Draw(spriteBatch);

            goldTextLabel.Draw(spriteBatch);
            goldValueLabel.Draw(spriteBatch);
            goldProductionLabel.Draw(spriteBatch);
            unitsTextLabel.Draw(spriteBatch);
            unitsValueLabel.Draw(spriteBatch);

            inspectorLabel.Draw(spriteBatch);
            selectedTileLabel.Draw(spriteBatch);
            tileStatusLabel.Draw(spriteBatch);
            tileInfoLabel.Draw(spriteBatch);
            spriteBatch.Draw(Textures.Mine, inspectorWindow.Bounds, Color.White);

            
            spriteBatch.End();
        }

    }
}
