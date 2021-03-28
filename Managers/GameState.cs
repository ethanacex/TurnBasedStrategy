using Microsoft.Xna.Framework;
using System;

namespace StrategyGame.Managers
{
    public enum PlayerTurn { Blue, Red };
    static class GameState
    {
        private static bool onMainMenu = false;
        private static bool gameIsRunning = false;
        private static bool gameIsActive = false;
        private static bool debugMode = false;
        private static bool allowMusic = true;
        private static bool allowSFX = true;
        private static bool windowInFocus = true;
        private static bool toggleGridLines = true;
        private static bool isFullScreen = false;
        private static bool isLowRes = false;
        private static bool isHighRes = true;

        private static PlayerTurn currentTurn = PlayerTurn.Blue;
        public static TimeSpan GameTrackPosition { get; internal set; } = TimeSpan.Zero;

        public static event EventHandler<EventArgs> MainMenuHandler;
        public static event EventHandler<EventArgs> InGameHandler;
        public static event EventHandler<EventArgs> GameIsRunningHandler;
        public static event EventHandler<EventArgs> DebugModeHandler;
        public static event EventHandler<EventArgs> WindowInFocusHandler;
        public static event EventHandler<EventArgs> AllowMusicHandler;
        public static event EventHandler<EventArgs> AllowSFXHandler;
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
        public static Color CurrentPlayerColor
        {
            get { return (CurrentPlayer == PlayerTurn.Blue) ? Color.DodgerBlue : Color.Red; }
        }
        public static string CurrentPlayerName
        {
            get { return (CurrentPlayer == PlayerTurn.Blue) ? "Blue" : "Red"; }
        }
        public static bool MenuIsActive { 
            get
            { 
                return onMainMenu;
            }
            set
            {
                if (onMainMenu != value)
                {
                    onMainMenu = value;
                    if (onMainMenu)
                        GameIsActive = false;
                    if (MainMenuHandler != null)
                        MainMenuHandler(onMainMenu, EventArgs.Empty);
                }

            }
        }
        public static bool GameIsActive
        {
            get
            {
                return gameIsActive;
            }
            set
            {
                if (gameIsActive != value)
                {
                    gameIsActive = value;
                    if (gameIsActive)
                        MenuIsActive = false;
                    if (InGameHandler != null)
                        InGameHandler(gameIsActive, EventArgs.Empty);
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
        public static bool DebugMode
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
                        Settings.BackdropColor = Color.Transparent;
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
        public static bool ToggleMusic
        {
            get
            {
                return allowMusic;
            }
            set
            {
                if (allowMusic != value)
                {
                    allowMusic = value;
                    if (AllowMusicHandler != null)
                        AllowMusicHandler(allowMusic, EventArgs.Empty);
                }
            }

        }
        public static bool ToggleSFX
        {
            get
            {
                return allowSFX;
            }
            set
            {
                if (allowSFX != value)
                {
                    allowSFX = value;
                    if (AllowSFXHandler != null)
                        AllowSFXHandler(allowSFX, EventArgs.Empty);
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
