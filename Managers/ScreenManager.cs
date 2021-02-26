using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TurnBasedStrategy.Media;
using TurnBasedStrategy.Screens;
using TurnBasedStrategy;

namespace TurnBasedStrategy.Managers
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
            currentScreen = screens.Peek();
        }

        public void PreviousScreen(object sender, EventArgs e)
        {
            screens.Pop();
            currentScreen = screens.Peek();
        }

        public void Options(object sender, EventArgs args)
        {
            currentScreen = new OptionsScreen();
            PushScreen(currentScreen);
            currentScreen.Initialize(this);
        }

        public void NewGame(object source, EventArgs args)
        {
            currentScreen = new GameScreen();
            PushScreen(currentScreen);
            currentScreen.Initialize(this);
        }

        public void EnableDisableScene(object sender, EventArgs e)
        {
            if (Configuration.MenuTexture == Scene.Empty)
            {
                Configuration.MenuTexture = Scene.TestPink;
            }
            else
            {
                Configuration.MenuTexture = Scene.Empty;
            }
            //foreach (var screen in screens)
            //{
            //    if (screen.Background.Name == Scene.TestPink.Name)
            //        screen.Background = Scene.Empty;
            //    else
            //        screen.Background = Scene.TestPink;
            //}
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
