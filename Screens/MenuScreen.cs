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

            newGameBtn = AddButtonCenter("New Game", viewCenter.X, viewCenter.Y - 75);
            optionsBtn = AddButtonCenter("Options", viewCenter.X, viewCenter.Y);
            exitGameBtn = AddButtonCenter("Exit", viewCenter.X, viewCenter.Y + 75);

            newGameBtn.ButtonPressed += screenManager.NewGame;
            optionsBtn.ButtonPressed += screenManager.Options;
            exitGameBtn.ButtonPressed += screenManager.ExitGame;

            newGameBtn.OnHover = Sounds.OnMenuHover;
            optionsBtn.OnHover = Sounds.OnMenuHover;
            exitGameBtn.OnHover = Sounds.OnMenuHover;

            newGameBtn.OnClick = Sounds.MenuForward;
            optionsBtn.OnClick = Sounds.MenuForward;
            exitGameBtn.OnClick = Sounds.MenuForward;
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
            spriteBatch.Draw(Configuration.BlackTexture, GraphicsManager.Viewport.Bounds, Color.White);
            newGameBtn.Draw(spriteBatch);
            optionsBtn.Draw(spriteBatch);
            exitGameBtn.Draw(spriteBatch);
            spriteBatch.End();
        }

        public Button AddButton(string title, int x, int y)
        {
            return new Button(title, x, y, Scene.Empty); ;
        }

        public Button AddButtonCenter(string title, int x, int y)
        {
            Button button = AddButton(title, x, y);
            GraphicsManager.CenterGameObjectOnScreen(button);
            return button;
        }


    }

}
