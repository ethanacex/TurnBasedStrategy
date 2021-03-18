using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Media;
using System;

namespace StrategyGame.Managers
{
    public enum PlayerTurn { Blue, Red };
    static class GameState
    {
        private static bool onMainMenu = false;
        private static bool gameIsRunning = false;
        private static bool debugMode = false;
        private static bool allowAudio = true;
        private static bool windowInFocus = true;
        private static bool toggleGridLines = true;
        private static bool isFullScreen = false;
        private static bool isLowRes = false;
        private static bool isHighRes = true;

        private static PlayerTurn currentTurn = PlayerTurn.Blue;

        public static event EventHandler<EventArgs> MainMenuHandler;
        public static event EventHandler<EventArgs> GameIsRunningHandler;
        public static event EventHandler<EventArgs> DebugModeHandler;
        public static event EventHandler<EventArgs> WindowInFocusHandler;
        public static event EventHandler<EventArgs> AllowAudioHandler;
        public static event EventHandler<EventArgs> FullscreenEventHandler;
        public static event EventHandler<EventArgs> LowResEventHandler;
        public static event EventHandler<EventArgs> HighResEventHandler;
        public static event EventHandler<EventArgs> PlayerTurnChangedHandler;

        public static PlayerTurn CurrentPlayer
        {
            get { return currentTurn; }
            set
            {
                if (currentTurn != value)
                {
                    currentTurn = value;
                    if (PlayerTurnChangedHandler != null)
                        PlayerTurnChangedHandler(null, EventArgs.Empty);
                }
            }
        }
        public static string CurrentPlayerName
        {
            get { return (CurrentPlayer == PlayerTurn.Blue) ? "Blue" : "Red"; }
        }
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
                    {
                        Settings.BackdropColor = Color.Pink;
                        Settings.GraphicsDeviceColor = Color.CornflowerBlue;
                    }
                    else
                    {
                        Settings.BackdropColor = Color.Black;
                        Settings.GraphicsDeviceColor = Color.Black;
                    }
                        
                }

            }
        }
        public static bool ToggleAudio
        {
            get
            {
                return allowAudio;
            }
            set
            {
                if (allowAudio != value)
                {
                    allowAudio = value;
                    if (AllowAudioHandler != null)
                        AllowAudioHandler(allowAudio, EventArgs.Empty);
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
        public static bool IsFullScreen
        {
            get { return isFullScreen; }
            set
            {
                if (isFullScreen != value)
                {
                    isFullScreen = value;
                    if (isFullScreen)
                    {
                        isHighRes = false;
                        isLowRes = false;
                    }
                    if (FullscreenEventHandler != null)
                        FullscreenEventHandler(isFullScreen, EventArgs.Empty);
                }
            }
        }
        public static bool IsHighResolution
        {
            get { return isHighRes; }
            set
            {
                if (isHighRes != value)
                {
                    isHighRes = value;
                    if (isHighRes)
                    {
                        isLowRes = false;
                        isFullScreen = false;
                    }
                    if (HighResEventHandler != null)
                        HighResEventHandler(isHighRes, EventArgs.Empty);
                }
            }
        }
        public static bool IsLowResolution
        {
            get { return isLowRes; }
            set
            {
                if (isLowRes != value)
                {
                    isLowRes = value;
                    if (isLowRes)
                    {
                        isHighRes = false;
                        isFullScreen = false;
                    }
                    if (LowResEventHandler != null)
                        LowResEventHandler(isLowRes, EventArgs.Empty);
                }
            }
        }
    }
}
