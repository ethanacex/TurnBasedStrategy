using StrategyGame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Media;
using StrategyGame.GUI;
using StrategyGame.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System;

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
        Panel messageLogPanel;

        Button inspectorButton;
        Button mapButton;
        Button endTurnButton;
        Button mainMenuButton;

        Label playerTurnLabel;
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

        private bool displayInspector = true;

        public void Initialize(ScreenManager screenManager)
        {
            this.screenManager = screenManager;

            // Subscribe to changes in resolution
            GraphicsManager.ResolutionChanged += Reinitialize;

            Viewport screen = GraphicsManager.Viewport;
            screenElements = new List<GameObject>();

            int marginX = (int)(screen.Width * 0.01);
            int marginY = (int)(screen.Height * 0.01);

            Point labelSize;

            // Initialize game area with margin around outer edge of screen
            gameArea = new Panel(new Point(screen.X + marginX, screen.Y + marginY), new Point(screen.Width - (marginX*2), screen.Height - (marginY*2)));

            // Debug - to be deleted when config is complete
            GraphicsManager.SetGameObjectColor(gameArea, Settings.BackdropColor);

            // Get the rendered font height so we can position the grid below it
            labelSize = GraphicsManager.GetLabelDimensions("Units: 10");
            Point gridPosition = new Point(gameArea.X, gameArea.Y + labelSize.Y);
            Settings.GridPosition = gridPosition;

            // Initialize Grid
            Settings.GridColumns = (int)Math.Floor((GraphicsManager.Viewport.Width * Settings.GridWidthPercent) / Settings.TileWidth);
            Settings.GridRows = (int)Math.Floor((GraphicsManager.Viewport.Height * Settings.GridHeightPercent) / Settings.TileHeight);

            if (GameState.IsLowResolution)
                Settings.GridColumns -= 3;

            grid = new Grid();
            grid.Initialize(Textures.Empty);

            // Initialize Panels
            statusPanel = new Panel(new Point(gameArea.X, gameArea.Y), new Point(grid.Width, grid.Top - gameArea.Y));
            messagePanel = new Panel(new Point(gameArea.X, grid.Bottom), new Point(grid.Width, gameArea.Height - (grid.Height + statusPanel.Height)));
            messageLogPanel = new Panel(new Point(messagePanel.X + 1, messagePanel.Y + 25), new Point(messagePanel.Width - 3, messagePanel.Height - 40));
            rightPanel = new Panel(new Point(grid.Right, gameArea.Y), new Point(25, gameArea.Height));

            inspectorPanel = new Panel(new Point(rightPanel.Right, gameArea.Y), new Point(gameArea.Width - (grid.Width + rightPanel.Width), gameArea.Height));
            inspectorWindow = new Panel(new Point(inspectorPanel.X, gridPosition.Y), new Point(350, 300));

            messageLogPanel.SetCustomBorder(3, Color.White);

            // Initialize elements on Status Bar
            Rectangle panel = statusPanel.Bounds;
            goldTextLabel = new Label("Gold: ", panel.Location.ToVector2(), Fonts.UI, Color.Cyan);
            goldValueLabel = new Label("120", new Vector2(goldTextLabel.Right, panel.Y), Fonts.UI, Color.White);
            goldProductionLabel = new Label(" (+75)", new Vector2(goldValueLabel.Right, panel.Y), Fonts.UI, Color.Yellow);

            unitsTextLabel = new Label("Units: ", new Vector2(statusPanel.Right - labelSize.X, panel.Y), Fonts.UI, Color.Cyan);
            unitsValueLabel = new Label("04", new Vector2(unitsTextLabel.Right, panel.Y), Fonts.UI, Color.White);

            labelSize = GraphicsManager.GetLabelDimensions(GameState.CurrentPlayerName + " Turn");
            playerTurnLabel = new Label(GameState.CurrentPlayerName + " Turn", new Vector2(panel.Center.X - labelSize.X / 2, panel.Y), Fonts.UI, Settings.TextColor);

            // Initialize elements on Inspector Panel
            panel = inspectorPanel.Bounds;
            inspectorButton = new Button("Inspector", panel.X, panel.Y, Fonts.UI);
            inspectorButton.Click = Audio.MenuForward;
            inspectorButton.DefaultLabelColor = Color.Yellow;
            inspectorButton.ButtonPressed += DisplayInspector;

            labelSize = GraphicsManager.GetLabelDimensions("Map");
            mapButton = new Button("Map", inspectorWindow.Right - labelSize.X, panel.Y, Fonts.UI);
            mapButton.Click = Audio.MenuForward;
            mapButton.DefaultLabelColor = Color.Yellow;
            mapButton.ButtonPressed += DisplayMap;

            if (displayInspector)
            {
                inspectorWindow.SetCustomBorder(3, Color.White);
                selectedStaticLabel = new Label("Selected: ", new Vector2(panel.X, inspectorWindow.Bottom + inspectorWindow.BorderWidth + 20), Fonts.Small, Color.Cyan);
                selectedVariableLabel = new Label("Gold Mine", new Vector2(selectedStaticLabel.Right, selectedStaticLabel.Y), Fonts.Small, Color.White);

                tileStatusStaticLabel = new Label("Status: ", new Vector2(panel.X, selectedStaticLabel.Bottom), Fonts.Small, Color.Cyan);
                tileStatusVariableLabel = new Label("Uncaptured", new Vector2(tileStatusStaticLabel.Right, tileStatusStaticLabel.Y), Fonts.Small, Color.White);

                tileInfoStaticLabel = new Label("Output: ", new Vector2(panel.X, tileStatusStaticLabel.Bottom), Fonts.Small, Color.Cyan);
                tileInfoVariableLabel = new Label("0 gold/turn", new Vector2(tileInfoStaticLabel.Right, tileInfoStaticLabel.Y), Fonts.Small, Color.White);
            }
            else
            {
                // Display Map
                selectedStaticLabel = new Label("", Vector2.Zero);
                selectedVariableLabel = new Label("", Vector2.Zero);

                tileStatusStaticLabel = new Label("", Vector2.Zero);
                tileStatusVariableLabel = new Label("", Vector2.Zero);

                tileInfoStaticLabel = new Label("", Vector2.Zero);
                tileInfoVariableLabel = new Label("", Vector2.Zero);
            }

            labelSize = GraphicsManager.GetLabelDimensions("EndTurn");
            endTurnButton = new Button("EndTurn", inspectorWindow.Right - labelSize.X, panel.Bottom - labelSize.Y, Fonts.UI);
            endTurnButton.DefaultLabelColor = Color.Yellow;
            endTurnButton.Click = Audio.MenuForward;
            endTurnButton.ButtonPressed += SwitchTurns;

            mainMenuButton = new Button("Menu", panel.Left, panel.Bottom - labelSize.Y, Fonts.UI);
            mainMenuButton.DefaultLabelColor = Color.Yellow;
            mainMenuButton.Click = Audio.MenuForward;
            mainMenuButton.ButtonPressed += MainMenu;

            // Initialize elements on Message Panel
            panel = messagePanel.Bounds;
            messageTitleLabel = new Label(" Message Log: ", new Vector2(panel.Left + 20, panel.Y + 10), Fonts.Small, Color.Cyan);
            messageLogLabel = new Label("This is an example battle log entry", new Vector2(panel.Left + 20, messageTitleLabel.Bottom + 10), Fonts.Small, Color.White);

            // Draw panels first
            screenElements.Add(statusPanel);
            screenElements.Add(rightPanel);
            screenElements.Add(messagePanel);
            screenElements.Add(inspectorPanel);
            screenElements.Add(inspectorWindow);
            screenElements.Add(messageLogPanel);

            // Draw elements next

            if (!GameState.IsLowResolution)
                screenElements.Add(playerTurnLabel);

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
            screenElements.Add(endTurnButton);
            screenElements.Add(mainMenuButton);

            screenElements.Add(grid);

        }

        private void MainMenu(object sender, EventArgs e)
        {
            screenManager.Menu();
        }

        private void DisplayMap(object sender, EventArgs e)
        {
            displayInspector = false;
            Initialize(screenManager);
        }

        private void DisplayInspector(object sender, EventArgs e)
        {
            displayInspector = true;
            Initialize(screenManager);
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

            // Placeholder images to be dynamically selected based on tile selection
            if (displayInspector)
                sb.Draw(Textures.Mine, inspectorWindow.Bounds, Color.White);
            else
                sb.Draw(Textures.WorldMap2, inspectorWindow.Bounds, Color.White);
            sb.End();
        }

        public void Reinitialize(object sender, EventArgs e)
        {
            Initialize(screenManager);
        }

        public void SwitchTurns(object sender, EventArgs e)
        {
            switch (GameState.CurrentPlayer)
            {
                case PlayerTurn.Blue: GameState.CurrentPlayer = PlayerTurn.Red; break;
                case PlayerTurn.Red: GameState.CurrentPlayer = PlayerTurn.Blue; break;
                default: break;
            }
            Initialize(screenManager);
        }
    }
}
