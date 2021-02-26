using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Media;
using StrategyGame.Screens;

namespace StrategyGame.Managers
{
    partial class ScreenManager
    {
        StrategyGame game;
        Stack<IScreen> screens;
        IScreen currentScreen;
        
        public ScreenManager(IScreen startScreen, StrategyGame game)
        {
            this.game = game;
            screens = new Stack<IScreen>();
            currentScreen = startScreen;
            PushScreen(currentScreen);
            currentScreen.Initialize(this);
        }

        public void PushScreen(IScreen screen)
        {
            screens.Push(screen);
        }

        public void PopScreen()
        {
            screens.Pop();
            if (screens.Count > 0)
                currentScreen = screens.Peek();
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
            currentScreen = new MenuScreen();
            PushScreen(currentScreen);
            currentScreen.Initialize(this);
        }

        public void Options(object sender, EventArgs args)
        {
            currentScreen = new OptionsScreen();
            PushScreen(currentScreen);
            currentScreen.Initialize(this);
        }

        public void NewGame(object source, EventArgs args)
        {
            if (Configuration.GameIsRunning)
            {
                while (screens.Count > 1)
                    screens.Pop();
                currentScreen = screens.Peek();
            }
            else
            {
                RemoveAllScreens();
                currentScreen = new GameScreen();
                PushScreen(currentScreen);
                currentScreen.Initialize(this);
                Configuration.GameIsRunning = true;
                Debug.WriteLine("GameRunning: " + Configuration.GameIsRunning);
            }
        }

        public void ToggleDebug(object sender, EventArgs e)
        {
            if (!Configuration.DebugColorMode)
            {
                Configuration.BlackTexture = Configuration.DebugTexture;
                Configuration.DebugColorMode = !Configuration.DebugColorMode;
            }
            else
            {
                Configuration.BlackTexture = Scene.Empty;
                Configuration.DebugColorMode = !Configuration.DebugColorMode;
            }
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
                Configuration.WindowInFocus = true;
                currentScreen.Update(gameTime);
            }
            else
                Configuration.WindowInFocus = false;
        }

    }
}
