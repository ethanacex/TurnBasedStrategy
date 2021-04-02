using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private Panel statusPanel;
        private Panel messagePanel;

        private Button inspectorButton;
        private Button mapButton;
        private Button endTurnButton;
        private Button mainMenuButton;

        private Label gameTitleLabel;
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

        private MiniMap map;
        private GameGrid grid;
        private BattleLog messageLog;
        private ViewFinder view;
        private DirectionalPad dPad;

        private bool displayMap = false;

        private void PurgeScreenElements()
        {
            screenElements = new List<GameObject>();
            mapPanelElements = new List<GameObject>();
        }

        private void AddAllScreenElements()
        {
            // Draw panels first
            screenElements.Add(view);
            screenElements.Add(grid);
            screenElements.Add(topPanel);
            screenElements.Add(bottomPanel);
            screenElements.Add(leftPanel);
            screenElements.Add(rightPanel);
            screenElements.Add(statusPanel);
            screenElements.Add(seperatorPanel);
            screenElements.Add(messagePanel);
            screenElements.Add(inspectorPanel);
            screenElements.Add(messageLog);
            screenElements.Add(inspectorWindow);

            // Screen Elements
            if (!GameState.IsLowResolution)
                screenElements.Add(gameTitleLabel);

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
            mapPanelElements.Add(dPad);
            mapPanelElements.Add(map);
        }

        private void InitViewFinder(InitType init)
        {
            // Get the rendered font height so we can position the ViewFinder below it
            Point labelSize = GraphicsManager.GetLabelDimensions("Units: 10");
            Point position = new Point(gameArea.X, gameArea.Y + labelSize.Y);

            Settings.ViewFinderXPercent = GameState.IsLowResolution ? 0.65 : 0.77;
            Settings.ViewFinderYPercent = GameState.IsLowResolution ? 0.55 : 0.66;

            Point size = new Point((int)(screen.Width * Settings.ViewFinderXPercent), (int)(screen.Height * Settings.ViewFinderYPercent));

            view = new ViewFinder(position, size);
            view.SetMainView();

            // If only updating this panel, we need to refresh the elements list here
            if (init == InitType.Isolated)
            {
                PurgeScreenElements();
                AddAllScreenElements();
            }
        }

        private void InitGrid()
        {
            // For now the level will be plain 'grass' this should be swappable dynamically later
            Texture2D grass = GraphicsManager.GetTextureOfColor(Color.Green);

            // Do not create a new instance of grid if one already exists
            if (grid == null)
            {
                grid = new GameGrid();
                grid.Initialize(grass);
            }
        }

        private void InitScreenRegions(InitType init)
        {
            topPanel = new Panel(Point.Zero, new Point(screen.Width, marginY));
            bottomPanel = new Panel(new Point(0, screen.Height - marginY), new Point(screen.Width, marginY));

            leftPanel = new Panel(new Point(topPanel.Left, topPanel.Bottom), new Point(marginX, screen.Height - (topPanel.Height + bottomPanel.Height)));
            rightPanel = new Panel(new Point(topPanel.Right - marginX, topPanel.Bottom), new Point(marginX, screen.Height - (topPanel.Height + bottomPanel.Height)));

            statusPanel = new Panel(new Point(gameArea.X, gameArea.Y), new Point(view.Width, view.Top - gameArea.Y));
            messagePanel = new Panel(new Point(gameArea.X, view.Bottom), new Point(view.Width, gameArea.Height - (view.Height + statusPanel.Height)));
            messageLog = new BattleLog(new Point(messagePanel.X + 1, messagePanel.Y + 25), new Point(messagePanel.Width - 3, messagePanel.Height - 40));
            seperatorPanel = new Panel(new Point(view.Right, gameArea.Y), new Point(25, gameArea.Height));

            inspectorPanel = new Panel(new Point(seperatorPanel.Right, gameArea.Y), new Point(gameArea.Width - (view.Width + seperatorPanel.Width), gameArea.Height));
            inspectorWindow = new Panel(new Point(inspectorPanel.X, view.Y), new Point(350, 300));
            GraphicsManager.CenterObjectOnPanel(inspectorWindow, inspectorPanel);

            messageLog.Initialize();

            GameState.NavigationFramePosition = inspectorWindow.Bounds.Location;

            // If only updating this panel, we need to refresh the elements list here
            if (init == InitType.Isolated)
            {
                PurgeScreenElements();
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
                PurgeScreenElements();
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
            endTurnButton.ButtonPressed += messageLog.UpdatePlayerTurnLabel;

            mainMenuButton = new Button("Menu", inspectorWindow.Left, inspectorPanel.Bottom - labelSize.Y, Fonts.UI);
            mainMenuButton.DefaultLabelColor = Color.Yellow;
            mainMenuButton.Click = Audio.MenuForward;
            mainMenuButton.ButtonPressed += MainMenu;

            map = new MiniMap(inspectorWindow.Bounds.Location, inspectorWindow.Bounds.Size);
            grid.MapUpdated += map.UpdatePreview;

            dPad = new DirectionalPad();
            var dPadMargin = GameState.IsLowResolution ? 10 : 100;
            dPad.Initialize(new Point(inspectorWindow.Left, inspectorWindow.Bottom + dPadMargin));

            // Subscribe minimap to movement events
            dPad.UpPressed += map.NavigateUp;
            dPad.DownPressed += map.NavigateDown;
            dPad.LeftPressed += map.NavigateLeft;
            dPad.RightPressed += map.NavigateRight;

            if (displayMap)
            {
                inspectorWindow.Texture = Textures.Transparent;
                inspectorWindow.SetCustomBorder(0, Color.Transparent);

                map.Initialize(grid.GameWorld);
                mapButton.DefaultLabelColor = Color.White;
                inspectorButton.DefaultLabelColor = Color.DimGray;

                selectedStaticLabel = new Label("", Vector2.Zero);
                selectedVariableLabel = new Label("", Vector2.Zero);

                tileStatusStaticLabel = new Label("", Vector2.Zero);
                tileStatusVariableLabel = new Label("", Vector2.Zero);

                tileInfoStaticLabel = new Label("", Vector2.Zero);
                tileInfoVariableLabel = new Label("", Vector2.Zero);
            }
            else
            {
                inspectorWindow.SetCustomBorder(3, Color.White);
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
                PurgeScreenElements();
                AddAllScreenElements();
            }
        }


        public void Initialize(ScreenManager screenManager)
        {
            this.screenManager = screenManager;

            screen = GraphicsManager.Viewport;
            PurgeScreenElements();

            // Subscribe to changes in resolution
            GraphicsManager.ResolutionChanged += Reinitialize;

            // Initialize game area with margin around outer edge of screen
            marginX = (int)(screen.Width * 0.01);
            marginY = (int)(screen.Height * 0.01);
            gameArea = new Panel(new Point(screen.X + marginX, screen.Y + marginY), new Point(screen.Width - (marginX * 2), screen.Height - (marginY * 2)));

            // We do not want each init function to update the list elements until all functions are complete
            InitViewFinder(InitType.Full);
            InitGrid();
            InitScreenRegions(InitType.Full);
            InitStatusBar(InitType.Full);
            InitInspectorPanel(InitType.Full);

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

        public void Update(GameTime gameTime)
        {
            foreach (var element in screenElements)
                element.Update(gameTime);
            if (displayMap)
                foreach (var element in mapPanelElements)
                    element.Update(gameTime);

            // DPad always needs to update, but only if it has not been updated already
            else
                dPad.Update(gameTime);
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
