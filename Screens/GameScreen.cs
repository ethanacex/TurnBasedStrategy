using StrategyGame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Media;
using StrategyGame.GUI;
using StrategyGame.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace StrategyGame.Screens
{
    class GameScreen : IScreen
    {
        ScreenManager screenManager;

        List<GameObject> screenElements;

        Panel gameArea;
        Panel inspectorPanel;
        Panel inspectorWindow;
        Panel statusPanel;
        Panel rightPanel;
        Panel messagePanel;

        Button inspectorButton;
        Button mapButton;

        Label messageTitleLabel;
        Label messageLogLabel;
        Label goldTextLabel;
        Label goldValueLabel;
        Label goldProductionLabel;
        Label unitsTextLabel;
        Label unitsValueLabel;
        Label selectedStaticLabel;
        Label selectedVariableLabel;
        Label tileStatusStaticLabel;
        Label tileStatusVariableLabel;
        Label tileInfoStaticLabel;
        Label tileInfoVariableLabel;

        Grid grid;

        public void Initialize(ScreenManager screenManager)
        {
            this.screenManager = screenManager;
            Viewport screen = GraphicsManager.Viewport;
            screenElements = new List<GameObject>();
            int marginX = (int)(screen.Width * 0.02);
            int marginY = (int)(screen.Height * 0.02);

            // Initialize game area with margin around outer edge of screen
            gameArea = new Panel(new Point(screen.X + marginX, screen.Y + marginY), new Point(screen.Width - (marginX*2), screen.Height - (marginY*2)));

            // Debug - to be deleted when config is complete
            GraphicsManager.SetGameObjectColor(gameArea, Settings.BackdropColor);

            // Get the rendered font height so we can position the grid below it
            Point labelDimensions = GraphicsManager.GetLabelDimensions("Units: 10");
            Point gridPosition = new Point(gameArea.X, gameArea.Y + labelDimensions.Y);
            Settings.GridPosition = gridPosition;

            // Initialize Grid
            grid = new Grid();
            grid.Initialize(Textures.Empty);

            // Initialize Panels
            statusPanel = new Panel(new Point(gameArea.X, gameArea.Y), new Point(grid.Width, grid.Top - gameArea.Y));
            messagePanel = new Panel(new Point(gameArea.X + 1, grid.Bottom + 10), new Point(grid.Width - 3, gameArea.Height - (grid.Height + statusPanel.Height + 10)));
            rightPanel = new Panel(new Point(grid.Right, gameArea.Y), new Point(15, gameArea.Height));

            inspectorPanel = new Panel(new Point(rightPanel.Right, gameArea.Y), new Point(gameArea.Width - (grid.Width + rightPanel.Width), gameArea.Height));
            inspectorWindow = new Panel(new Point(inspectorPanel.X, gridPosition.Y), new Point(350, 300));
            inspectorWindow.SetCustomBorder(3, Color.White);
            messagePanel.SetCustomBorder(3, Color.White);

            // Initialize elements on Status Bar
            Rectangle panel = statusPanel.Bounds;
            goldTextLabel = new Label("Gold: ", panel.Location.ToVector2(), Fonts.UI, Color.Cyan);
            goldValueLabel = new Label("120", new Vector2(goldTextLabel.Right, panel.Y), Fonts.UI, Color.White);
            goldProductionLabel = new Label(" (+75)", new Vector2(goldValueLabel.Right, panel.Y), Fonts.UI, Color.Yellow);
            unitsTextLabel = new Label("Units: ", new Vector2(statusPanel.Right-labelDimensions.X, panel.Y), Fonts.UI, Color.Cyan);
            unitsValueLabel = new Label("04", new Vector2(unitsTextLabel.Right, panel.Y), Fonts.UI, Color.White);

            // Initialize elements on Inspector Panel
            panel = inspectorPanel.Bounds;
            inspectorButton = new Button("Inspector", panel.X, panel.Y, Fonts.UI);
            mapButton = new Button("Map", inspectorWindow.Right - GraphicsManager.GetLabelDimensions("Map").X, panel.Y, Fonts.UI);

            inspectorButton.Click = Audio.MenuForward;
            mapButton.Click = Audio.MenuForward;

            inspectorButton.DefaultLabelColor = Color.Yellow;
            mapButton.DefaultLabelColor = Color.Yellow;

            selectedStaticLabel = new Label("Selected: ", new Vector2(panel.X, inspectorWindow.Bottom + inspectorWindow.BorderWidth + 20), Fonts.Small, Color.Cyan);
            selectedVariableLabel = new Label("Gold Mine", new Vector2(selectedStaticLabel.Right, selectedStaticLabel.Y), Fonts.Small, Color.White);
            
            tileStatusStaticLabel = new Label("Status: ", new Vector2(panel.X, selectedStaticLabel.Bottom), Fonts.Small, Color.Cyan);
            tileStatusVariableLabel = new Label("Uncaptured", new Vector2(tileStatusStaticLabel.Right, tileStatusStaticLabel.Y), Fonts.Small, Color.White);
            
            tileInfoStaticLabel = new Label("Output: ", new Vector2(panel.X, tileStatusStaticLabel.Bottom), Fonts.Small, Color.Cyan);
            tileInfoVariableLabel = new Label("0 gold/turn", new Vector2(tileInfoStaticLabel.Right, tileInfoStaticLabel.Y), Fonts.Small, Color.White);

            // Initialize elements on Message Panel
            panel = messagePanel.Bounds;
            messageTitleLabel = new Label("Message Log:", new Vector2(panel.Left + 20, panel.Y + 10), Fonts.Small, Color.Cyan);
            messageLogLabel = new Label("This is an example battle log entry", new Vector2(panel.Left + 20, messageTitleLabel.Bottom), Fonts.Small, Color.White);

            // Draw panels first
            screenElements.Add(statusPanel);
            screenElements.Add(rightPanel);
            screenElements.Add(messagePanel);
            screenElements.Add(inspectorPanel);
            screenElements.Add(inspectorWindow);

            // Draw elements next
            screenElements.Add(messageTitleLabel);
            screenElements.Add(messageLogLabel);
            screenElements.Add(inspectorButton);
            screenElements.Add(mapButton);
            screenElements.Add(goldTextLabel);
            screenElements.Add(goldValueLabel);
            screenElements.Add(goldProductionLabel);
            screenElements.Add(unitsTextLabel);
            screenElements.Add(unitsValueLabel);
            screenElements.Add(selectedStaticLabel);
            screenElements.Add(tileStatusStaticLabel);
            screenElements.Add(tileInfoStaticLabel);
            screenElements.Add(selectedVariableLabel);
            screenElements.Add(tileStatusVariableLabel);
            screenElements.Add(tileInfoVariableLabel);

            screenElements.Add(grid);

        }

        public void Update(GameTime gameTime)
        {
            foreach (var element in screenElements)
                element.Update(gameTime);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            foreach (var element in screenElements)
                element.Draw(sb);
            sb.Draw(Textures.Mine, inspectorWindow.Bounds, Color.White);
            sb.End();
        }

    }
}
