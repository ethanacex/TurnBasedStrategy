using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Media;
using System;

namespace StrategyGame.Managers
{
    static class GameState
    {
        private static bool onMainMenu = false;
        private static bool gameIsRunning = false;
        private static bool debugMode = false;
        private static bool windowInFocus = true;
        private static bool toggleGridLines = true;

        public static event EventHandler<EventArgs> MainMenuHandler;
        public static event EventHandler<EventArgs> GameIsRunningHandler;
        public static event EventHandler<EventArgs> DebugModeHandler;
        public static event EventHandler<EventArgs> WindowInFocusHandler;

        public static bool IsOnMenuScreen { 
            get
            { 
                return onMainMenu;
            }
            set
            {
                if (onMainMenu != value)
                {
                    onMainMenu = value;
                    if (MainMenuHandler != null)
                        MainMenuHandler(onMainMenu, EventArgs.Empty);
                }

            }
        }
        public static bool GameIsRunning
        {
            get
            {
                return gameIsRunning;
            }
            set
            {
                if (gameIsRunning != value)
                {
                    gameIsRunning = value;
                    if (GameIsRunningHandler != null)
                        GameIsRunningHandler(gameIsRunning, EventArgs.Empty);
                }

            }
        }
        public static bool DebugColorMode
        {
            get
            {
                return debugMode;
            }
            set
            {
                if (debugMode != value)
                {
                    debugMode = value;
                    if (DebugModeHandler != null)
                        DebugModeHandler(debugMode, EventArgs.Empty);
                    if (debugMode)
                        Backdrop = Textures.TestPink;
                    else
                        Backdrop = Textures.Empty;
                }

            }
        }
        public static bool WindowInFocus
        {
            get
            {
                return windowInFocus;
            }
            set
            {
                if (windowInFocus != value)
                {
                    windowInFocus = value;
                    if (WindowInFocusHandler != null)
                        WindowInFocusHandler(windowInFocus, EventArgs.Empty);
                }
            }
        }
        public static bool ToggleGridLines
        {
            get { return toggleGridLines; }
            set
            {
                if (toggleGridLines != value)
                {
                    toggleGridLines = value;
                    Settings.BorderColor = toggleGridLines ? Color.Black : Color.Transparent;
                }
            }
        }

        public static Texture2D Backdrop { get; set; } = Textures.Empty;
    }
}
