using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Media;
using StrategyGame.IO;
using StrategyGame.Managers;

namespace StrategyGame.Screens
{
    class MenuScreen : IScreen
    {
        ScreenManager screenManager;

        Button newGameBtn;
        Button optionsBtn;
        Button exitGameBtn;

        public void Initialize(ScreenManager screenManager)
        {
            this.screenManager = screenManager;

            Point viewCenter = GraphicsManager.Viewport.Bounds.Center;

            newGameBtn = AddButtonCenter("New Game", viewCenter.X, viewCenter.Y - 25);
            optionsBtn = AddButtonCenter("Options", viewCenter.X, viewCenter.Y + 50);
            exitGameBtn = AddButtonCenter("Exit", viewCenter.X, viewCenter.Y + 125);

            newGameBtn.ButtonPressed += screenManager.NewGame;
            optionsBtn.ButtonPressed += screenManager.Options;
            exitGameBtn.ButtonPressed += screenManager.ExitGame;

            newGameBtn.Hover = Audio.OnMenuHover;
            optionsBtn.Hover = Audio.OnMenuHover;
            exitGameBtn.Hover = Audio.OnMenuHover;

            newGameBtn.Click = Audio.MenuForward;
            optionsBtn.Click = Audio.MenuForward;
            exitGameBtn.Click = Audio.MenuForward;
        }

        public void Update(GameTime gameTime)
        {
            newGameBtn.Update(gameTime);
            optionsBtn.Update(gameTime);
            exitGameBtn.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Configuration.BackdropTexture, GraphicsManager.Viewport.Bounds, Color.White);
            spriteBatch.Draw(Textures.Logo, new Rectangle(0, 0, Textures.Logo.Width, Textures.Logo.Height), Configuration.TextureColor);
            newGameBtn.Draw(spriteBatch);
            optionsBtn.Draw(spriteBatch);
            exitGameBtn.Draw(spriteBatch);
            spriteBatch.End();
        }

        public Button AddButton(string title, int x, int y)
        {
            return new Button(title, x, y, Textures.Empty); ;
        }

        public Button AddButtonCenter(string title, int x, int y)
        {
            Button button = AddButton(title, x, y);
            GraphicsManager.CenterGameObjectOnScreen(button);
            return button;
        }


    }

}
