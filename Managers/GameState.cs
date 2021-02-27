using Microsoft.Xna.Framework;
using System;

namespace StrategyGame.Managers
{
    static class GameState
    {
        private static bool onMainMenu = false;
        private static bool gameIsRunning = false;
        private static bool debugMode = false;
        private static bool windowInFocus = true;
        private static bool toggleBorder = true;

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
                gameIsRunning = value;
                if (GameIsRunningHandler != null)
                    GameIsRunningHandler(gameIsRunning, EventArgs.Empty);
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
                debugMode = value;
                if (DebugModeHandler != null)
                    DebugModeHandler(debugMode, EventArgs.Empty);
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
                windowInFocus = value;
                if (WindowInFocusHandler != null)
                    WindowInFocusHandler(windowInFocus, EventArgs.Empty);
            }
        }


        public static bool ToggleBorder
        {
            get { return toggleBorder; }
            set
            {
                toggleBorder = value;
                Configuration.BorderColor = toggleBorder ? Color.Black : Color.Transparent;
            }
        }
    }
}
