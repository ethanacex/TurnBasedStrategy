using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Core;
using StrategyGame.GUI;
using StrategyGame.Managers;
using StrategyGame.Media;
using System;
using System.Collections.Generic;

namespace StrategyGame.Screens
{
    class GameScreen : IScreen
    {
        private ScreenManager screenManager;

        private Viewport screen;
        private int marginX;
        private int marginY;

        private List<GameObject> screenElements;
        private List<GameObject> mapPanelElements;

        private enum InitType { Full, Isolated };

        private Panel gameArea;
        private Panel topPanel;
        private Panel bottomPanel;
        private Panel leftPanel;
        private Panel rightPanel;
        private Panel seperatorPanel;
        private Panel inspectorPanel;
        private Panel inspectorWindow;
        private Panel miniNav;
        private Panel statusPanel;
        private Panel messagePanel;
        private Panel messageLogPanel;
        private Panel playerTurnPanel;
        private Panel viewFinder;

        private Button inspectorButton;
        private Button mapButton;
        private Button endTurnButton;
        private Button mainMenuButton;

        private Label gameTitleLabel;
        private Label playerTurnLabel;
        private Label messageTitleLabel;
        private Label messageLogLabel;
        private Label goldTextLabel;
        private Label goldValueLabel;
        private Label goldProductionLabel;
        private Label unitsTextLabel;
        private Label unitsValueLabel;
        private Label selectedStaticLabel;
        private Label selectedVariableLabel;
        private Label tileStatusStaticLabel;
        private Label tileStatusVariableLabel;
        private Label tileInfoStaticLabel;
        private Label tileInfoVariableLabel;

        private GameGrid grid;
        private MapNav dPad;

        private Point miniNavDimensions;
        private Point miniNavPosition;

        private bool displayMap = false;

        private void FlushScreenElements()
        {
            screenElements = new List<GameObject>();
            mapPanelElements = new List<GameObject>();
        }

        private void AddAllScreenElements()
        {
            // Draw panels first
            screenElements.Add(viewFinder);
            screenElements.Add(grid);
            screenElements.Add(topPanel);
            screenElements.Add(bottomPanel);
            screenElements.Add(leftPanel);
            screenElements.Add(rightPanel);
            screenElements.Add(statusPanel);
            screenElements.Add(seperatorPanel);
            screenElements.Add(messagePanel);
            screenElements.Add(inspectorPanel);
            screenElements.Add(messageLogPanel);
            screenElements.Add(playerTurnPanel);
            screenElements.Add(inspectorWindow);

            // Screen Elements
            if (!GameState.IsLowResolution)
                screenElements.Add(gameTitleLabel);

            screenElements.Add(messageTitleLabel);
            screenElements.Add(playerTurnLabel);
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

            // Map Panel Elements
            mapPanelElements.Add(miniNav);
            mapPanelElements.Add(dPad);
        }

        private void InitViewFinder(InitType init)
        {
            // Get the rendered font height so we can position the ViewFinder below it
            Point labelSize = GraphicsManager.GetLabelDimensions("Units: 10");
            Point viewPosition = new Point(gameArea.X, gameArea.Y + labelSize.Y);

            if (GameState.IsLowResolution)
            {
                Settings.ViewFinderXPercent = 0.65;
                Settings.ViewFinderYPercent = 0.55;
            }
            else
            {
                Settings.ViewFinderXPercent = 0.77;
                Settings.ViewFinderYPercent = 0.66;
            }

            viewFinder = new Panel(viewPosition, new Point((int)(screen.Width * Settings.ViewFinderXPercent), (int)(screen.Height * Settings.ViewFinderYPercent)));
            viewFinder.Texture = Textures.Black;

            // Update ViewFinder dimensions in Settings so that other classes can query
            Settings.ViewFinder = viewFinder.Bounds;

            // If only updating this panel, we need to refresh the elements list here
            if (init == InitType.Isolated)
            {
                FlushScreenElements();
                AddAllScreenElements();
            }
        }

        private void InitGrid()
        {
            // Do not create a new instance of grid if one already exists
            if (grid == null)
            {
                grid = new GameGrid();
                grid.Initialize(Settings.GridPosition, Textures.Transparent);
            }
        }

        private void InitScreenRegions(InitType init)
        {
            topPanel = new Panel(Point.Zero, new Point(screen.Width, marginY));
            bottomPanel = new Panel(new Point(0, screen.Height - marginY), new Point(screen.Width, marginY));

            leftPanel = new Panel(new Point(topPanel.Left, topPanel.Bottom), new Point(marginX, screen.Height - (topPanel.Height + bottomPanel.Height)));
            rightPanel = new Panel(new Point(topPanel.Right - marginX, topPanel.Bottom), new Point(marginX, screen.Height - (topPanel.Height + bottomPanel.Height)));

            statusPanel = new Panel(new Point(gameArea.X, gameArea.Y), new Point(viewFinder.Width, viewFinder.Top - gameArea.Y));
            messagePanel = new Panel(new Point(gameArea.X, viewFinder.Bottom), new Point(viewFinder.Width, gameArea.Height - (viewFinder.Height + statusPanel.Height)));
            messageLogPanel = new Panel(new Point(messagePanel.X + 1, messagePanel.Y + 25), new Point(messagePanel.Width - 3, messagePanel.Height - 40));
            seperatorPanel = new Panel(new Point(viewFinder.Right, gameArea.Y), new Point(25, gameArea.Height));

            inspectorPanel = new Panel(new Point(seperatorPanel.Right, gameArea.Y), new Point(gameArea.Width - (viewFinder.Width + seperatorPanel.Width), gameArea.Height));
            inspectorWindow = new Panel(new Point(inspectorPanel.X, viewFinder.Y), new Point(350, 300));
            GraphicsManager.CenterObjectOnPanel(inspectorWindow, inspectorPanel);

            messageLogPanel.SetCustomBorder(3, Color.White);
            inspectorWindow.SetCustomBorder(3, Color.White);
            inspectorWindow.Texture = Textures.Transparent;

            // If only updating this panel, we need to refresh the elements list here
            if (init == InitType.Isolated)
            {
                FlushScreenElements();
                AddAllScreenElements();
            }
        }

        private void InitStatusBar(InitType init)
        {
            Point labelSize = GraphicsManager.GetLabelDimensions("Units: 10");
            goldTextLabel = new Label("Gold: ", statusPanel.Bounds.Location.ToVector2(), Fonts.UI, Color.Cyan);
            goldValueLabel = new Label("120", new Vector2(goldTextLabel.Right, statusPanel.Y), Fonts.UI, Color.White);
            goldProductionLabel = new Label(" (+75)", new Vector2(goldValueLabel.Right, statusPanel.Y), Fonts.UI, Color.Yellow);

            gameTitleLabel = new Label("Regicide", new Vector2(goldProductionLabel.Right, statusPanel.Y), Fonts.UI, Color.White);
            GraphicsManager.CenterObjectOnPanel(gameTitleLabel, statusPanel);

            unitsTextLabel = new Label("Units: ", new Vector2(statusPanel.Right - labelSize.X, statusPanel.Y), Fonts.UI, Color.Cyan);
            unitsValueLabel = new Label("04", new Vector2(unitsTextLabel.Right, statusPanel.Y), Fonts.UI, Color.White);

            // If only updating this panel, we need to refresh the elements list here
            if (init == InitType.Isolated)
            {
                FlushScreenElements();
                AddAllScreenElements();
            }
        }

        private void InitInspectorPanel(InitType init)
        {
            Point labelSize;
            inspectorButton = new Button("Inspector", inspectorWindow.X, inspectorPanel.Y, Fonts.UI);
            inspectorButton.Click = Audio.MenuForward;
            inspectorButton.ButtonPressed += DisplayInspector;

            labelSize = GraphicsManager.GetLabelDimensions("Map");
            mapButton = new Button("Map", inspectorWindow.Right - labelSize.X, inspectorPanel.Y, Fonts.UI);
            mapButton.Click = Audio.MenuForward;
            mapButton.ButtonPressed += DisplayMap;

            labelSize = GraphicsManager.GetLabelDimensions("EndTurn");
            endTurnButton = new Button("EndTurn", inspectorWindow.Right - labelSize.X, inspectorPanel.Bottom - labelSize.Y, Fonts.UI);
            endTurnButton.DefaultLabelColor = Color.Yellow;
            endTurnButton.Click = Audio.MenuForward;
            endTurnButton.ButtonPressed += SwitchTurns;

            mainMenuButton = new Button("Menu", inspectorWindow.Left, inspectorPanel.Bottom - labelSize.Y, Fonts.UI);
            mainMenuButton.DefaultLabelColor = Color.Yellow;
            mainMenuButton.Click = Audio.MenuForward;
            mainMenuButton.ButtonPressed += MainMenu;

            // Calculate current screen resolution in ratio to the grid so mini-viewFinder size can be calculated
            var ratioWidth = (double)screen.Width / Settings.GridWidth;
            var ratioHeight = (double)screen.Height / Settings.GridHeight;
            miniNavDimensions = new Point((int)(inspectorWindow.Width * ratioWidth), (int)(inspectorWindow.Height * ratioHeight));
            miniNavPosition = new Point(inspectorWindow.Bounds.Center.X - (miniNavDimensions.X / 2), inspectorWindow.Bottom - miniNavDimensions.Y);

            if (displayMap)
            {
                mapButton.DefaultLabelColor = Color.White;
                inspectorButton.DefaultLabelColor = Color.DimGray;

                inspectorWindow.Texture = GraphicsManager.GetTextureOfColor(Color.Green);
                miniNav = new Panel(miniNavPosition, miniNavDimensions);
                miniNav.Texture = Textures.Transparent;
                miniNav.SetCustomBorder(3, Color.Red);

                dPad = new MapNav();
                var dPadMargin = GameState.IsLowResolution ? 10 : 100;
                dPad.Initialize(new Point(inspectorWindow.Left, inspectorWindow.Bottom + dPadMargin));

                // Subscribe to button presses
                dPad.UpPressed += NavigateUp;
                dPad.DownPressed += NavigateDown;
                dPad.LeftPressed += NavigateLeft;
                dPad.RightPressed += NavigateRight;

                selectedStaticLabel = new Label("", Vector2.Zero);
                selectedVariableLabel = new Label("", Vector2.Zero);

                tileStatusStaticLabel = new Label("", Vector2.Zero);
                tileStatusVariableLabel = new Label("", Vector2.Zero);

                tileInfoStaticLabel = new Label("", Vector2.Zero);
                tileInfoVariableLabel = new Label("", Vector2.Zero);
            }
            else
            {
                mapButton.DefaultLabelColor = Color.DimGray;
                inspectorButton.DefaultLabelColor = Color.White;

                // Placeholder images to be dynamically selected based on tile selection
                inspectorWindow.Texture = Textures.Mine;

                selectedStaticLabel = new Label("Selected: ", new Vector2(inspectorWindow.X, inspectorWindow.Bottom + inspectorWindow.BorderWidth + 20), Fonts.Small, Color.Cyan);
                selectedVariableLabel = new Label("Gold Mine", new Vector2(selectedStaticLabel.Right, selectedStaticLabel.Y), Fonts.Small, Color.White);

                tileStatusStaticLabel = new Label("Status: ", new Vector2(inspectorWindow.X, selectedStaticLabel.Bottom), Fonts.Small, Color.Cyan);
                tileStatusVariableLabel = new Label("Uncaptured", new Vector2(tileStatusStaticLabel.Right, tileStatusStaticLabel.Y), Fonts.Small, Color.White);

                tileInfoStaticLabel = new Label("Output: ", new Vector2(inspectorWindow.X, tileStatusStaticLabel.Bottom), Fonts.Small, Color.Cyan);
                tileInfoVariableLabel = new Label("0 gold/turn", new Vector2(tileInfoStaticLabel.Right, tileInfoStaticLabel.Y), Fonts.Small, Color.White);
            }

            // If only updating this panel, we need to refresh the elements list here
            if (init == InitType.Isolated)
            {
                FlushScreenElements();
                AddAllScreenElements();
            }
        }

        private void NavigateRight(object sender, EventArgs e)
        {
            miniNavPosition.X += 2;
            GraphicsManager.MoveGameObject(miniNav, miniNavPosition);
        }

        private void NavigateLeft(object sender, EventArgs e)
        {
            miniNavPosition.X -= 2;
            GraphicsManager.MoveGameObject(miniNav, miniNavPosition);
        }

        private void NavigateDown(object sender, EventArgs e)
        {
            miniNavPosition.Y += 2;
            GraphicsManager.MoveGameObject(miniNav, miniNavPosition);
        }

        private void NavigateUp(object sender, EventArgs e)
        {
            miniNavPosition.Y -= 2;
            GraphicsManager.MoveGameObject(miniNav, miniNavPosition);
        }

        private void InitMessagePanel(InitType init)
        {
            Point labelSize;
            messageTitleLabel = new Label(" Message Log: ", new Vector2(messagePanel.Left + 20, messagePanel.Y + 10), Fonts.Small, Color.Cyan);
            messageLogLabel = new Label("This is an example battle log entry", new Vector2(messagePanel.Left + 20, messageTitleLabel.Bottom + 10), Fonts.Small, Color.White);

            labelSize = Fonts.Small.MeasureString(" Blue's Turn ").ToPoint();
            playerTurnPanel = new Panel(new Point(messagePanel.Right - (labelSize.X + 20), messageTitleLabel.Y), labelSize);
            labelSize = Fonts.Small.MeasureString(" " + GameState.CurrentPlayerName + "s Turn ").ToPoint();
            playerTurnLabel = new Label(" " + GameState.CurrentPlayerName + "s Turn ", new Vector2(playerTurnPanel.Bounds.Center.X - (labelSize.X / 2), messageTitleLabel.Y), Fonts.Small, GameState.CurrentPlayerColor);

            // If only updating this panel, we need to refresh the elements list here
            if (init == InitType.Isolated)
            {
                FlushScreenElements();
                AddAllScreenElements();
            }
        }

        public void Initialize(ScreenManager screenManager)
        {
            this.screenManager = screenManager;

            screen = GraphicsManager.Viewport;
            FlushScreenElements();

            // Subscribe to changes in resolution
            GraphicsManager.ResolutionChanged += Reinitialize;

            // Initialize game area with margin around outer edge of screen
            marginX = (int)(screen.Width * 0.01);
            marginY = (int)(screen.Height * 0.01);
            gameArea = new Panel(new Point(screen.X + marginX, screen.Y + marginY), new Point(screen.Width - (marginX * 2), screen.Height - (marginY * 2)));

            // Grid should be positioned in the center of the viewfinder at first launch
            Settings.GridPosition = new Point(0 - (Settings.GridWidth / 2), 0 - (Settings.GridHeight / 2));

            // We do not want each init function to update the list elements until all functions are complete
            InitViewFinder(InitType.Full);
            InitGrid();
            InitScreenRegions(InitType.Full);
            InitStatusBar(InitType.Full);
            InitInspectorPanel(InitType.Full);
            InitMessagePanel(InitType.Full);

            AddAllScreenElements();
        }

        private void MainMenu(object sender, EventArgs e)
        {
            screenManager.Menu();
        }

        private void DisplayMap(object sender, EventArgs e)
        {
            displayMap = true;
            InitInspectorPanel(InitType.Isolated);
        }

        private void DisplayInspector(object sender, EventArgs e)
        {
            displayMap = false;
            InitInspectorPanel(InitType.Isolated);
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
            InitMessagePanel(InitType.Isolated);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var element in screenElements)
                element.Update(gameTime);
            if (displayMap)
                foreach (var element in mapPanelElements)
                    element.Update(gameTime);
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (var element in screenElements)
                element.Draw(sb);

            if (displayMap)
                foreach (var element in mapPanelElements)
                    element.Draw(sb);
        }
    }
}
