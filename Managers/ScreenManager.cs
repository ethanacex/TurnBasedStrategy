using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Screens;

namespace StrategyGame.Managers
{
    class ScreenManager
    {
        StrategyGame game;
        Stack<IScreen> screens;
        IScreen currentScreen;
        
        public ScreenManager(IScreen startScreen, StrategyGame game)
        {
            this.game = game;
            screens = new Stack<IScreen>();
            SetCurrentScreen(startScreen);
            PushScreen(currentScreen);
            currentScreen.Initialize(this);
        }

        private void SetCurrentScreen(IScreen startScreen)
        {
            currentScreen = startScreen;
            if (currentScreen is GameScreen)
            {
                GameState.GameIsRunning = true;
                GameState.GameIsActive = true;
            }
            else
                GameState.MenuIsActive = true;
        }

        public void PushScreen(IScreen screen)
        {
            screens.Push(screen);
        }

        public void PopScreen()
        {
            screens.Pop();
            if (screens.Count > 0)
                SetCurrentScreen(screens.Peek());
            else
                game.Exit();
        }

        public void RemoveAllScreens()
        {
            while (screens.Count > 0)
                screens.Pop();
        }

        public void PreviousScreen(object sender, EventArgs e)
        {
            PopScreen();
        }

        public void Menu()
        {
            SetCurrentScreen(new MenuScreen());
            PushScreen(currentScreen);
            currentScreen.Initialize(this);
        }

        public void Options(object sender, EventArgs args)
        {
            SetCurrentScreen(new OptionsScreen());
            PushScreen(currentScreen);
            currentScreen.Initialize(this);
        }

        public void NewGame(object source, EventArgs args)
        {
            if (GameState.GameIsRunning)
            {
                while (screens.Count > 1)
                    screens.Pop();
                SetCurrentScreen(screens.Peek());
            }
            else
            {
                RemoveAllScreens();
                SetCurrentScreen(new GameScreen());
                PushScreen(currentScreen);
                currentScreen.Initialize(this);
            }
        }

        internal void ToggleFullscreen(object sender, EventArgs e)
        {
            GameState.IsFullScreen = true;
        }

        internal void ToggleHighRes(object sender, EventArgs e)
        {
            GameState.IsHighResolution = true;
        }

        internal void ToggleLowRes(object sender, EventArgs e)
        {
            GameState.IsLowResolution = true;
        }

        public void ToggleDebug(object sender, EventArgs e)
        {
            GameState.DebugMode = !GameState.DebugMode;
        }

        public void ToggleMusic(object sender, EventArgs e)
        {
            GameState.ToggleMusic = !GameState.ToggleMusic;
        }

        public void ToggleSFX(object sender, EventArgs e)
        {
            GameState.ToggleSFX = !GameState.ToggleSFX;
        }

        public void ExitGame(object source, EventArgs args)
        {
            game.Exit();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            if (game.IsActive)
            {
                GameState.WindowInFocus = true;
                currentScreen.Update(gameTime);
            }
            else
                GameState.WindowInFocus = false;
        }

    }
}
