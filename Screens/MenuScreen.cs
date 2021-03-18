using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Media;
using StrategyGame.IO;
using StrategyGame.Managers;
using System;

namespace StrategyGame.Screens
{
    class MenuScreen : IScreen
    {
        ScreenManager screenManager;
        Rectangle logoPosition;

        Button newGameBtn;
        Button optionsBtn;
        Button exitGameBtn;

        public void Initialize(ScreenManager screenManager)
        {
            this.screenManager = screenManager;

            // Subscribe to changes in resolution
            GraphicsManager.ResolutionChanged += Reinitialize;

            Point screenCenter = GraphicsManager.Viewport.Bounds.Center;

            newGameBtn = CreateCenterAlignedButton("New Game", screenCenter.X, screenCenter.Y - 25);
            optionsBtn = CreateCenterAlignedButton("Options", screenCenter.X, screenCenter.Y + 50);
            exitGameBtn = CreateCenterAlignedButton("Exit", screenCenter.X, screenCenter.Y + 125);

            logoPosition = GraphicsManager.GetCenterXDrawableRegion(Textures.Logo.Bounds);
            logoPosition.Y = newGameBtn.Y - 175;

            newGameBtn.ButtonPressed += this.screenManager.NewGame;
            optionsBtn.ButtonPressed += this.screenManager.Options;
            exitGameBtn.ButtonPressed += this.screenManager.ExitGame;

            newGameBtn.Hover = Audio.OnMenuHover;
            optionsBtn.Hover = Audio.OnMenuHover;
            exitGameBtn.Hover = Audio.OnMenuHover;

            newGameBtn.Click = Audio.MenuForward;
            optionsBtn.Click = Audio.MenuForward;
            exitGameBtn.Click = Audio.MenuForward;
        }

        public void Update(GameTime gameTime)
        {
            if (GameState.GameIsRunning && newGameBtn.Label.Body.Equals("New Game"))
                newGameBtn.Label.Body = "Continue";
            newGameBtn.Update(gameTime);
            optionsBtn.Update(gameTime);
            exitGameBtn.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(GraphicsManager.GetTextureOfColor(Settings.BackdropColor), GraphicsManager.Viewport.Bounds, Settings.TextureColor);
            spriteBatch.Draw(Textures.Logo, logoPosition, Settings.TextureColor);
            newGameBtn.Draw(spriteBatch);
            optionsBtn.Draw(spriteBatch);
            exitGameBtn.Draw(spriteBatch);
            spriteBatch.End();
        }

        private Button CreateButton(string title, int x, int y)
        {
            return new Button(title, x, y, Textures.Empty);
        }

        private Button CreateCenterAlignedButton(string title, int x, int y)
        {
            Button button = CreateButton(title, x, y);
            GraphicsManager.CenterGameObjectX(button);
            return button;
        }

        public void Reinitialize(object sender, EventArgs e)
        {
            Initialize(screenManager);
        }
    }

}
