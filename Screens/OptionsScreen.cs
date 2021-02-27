using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Media;
using StrategyGame.IO;
using StrategyGame.Screens;

namespace StrategyGame.Managers
{
    class OptionsScreen : IScreen
    {
        ScreenManager screenManager;
        Label screenTitle;
        Button debugButton;
        Button backButton;

        public void Initialize(ScreenManager screenManager)
        {
            this.screenManager = screenManager;

            Point viewCenter = GraphicsManager.Viewport.Bounds.Center;

            debugButton = AddButtonCenter("Toggle Debug", viewCenter.X, viewCenter.Y - 75);
            backButton = AddButtonCenter("Back", viewCenter.X, viewCenter.Y);
            screenTitle = new Label("Options Menu", new Vector2(viewCenter.X, viewCenter.Y - 200));

            GraphicsManager.CenterGameObjectX(screenTitle);

            backButton.ButtonPressed += screenManager.PreviousScreen;
            debugButton.ButtonPressed += screenManager.ToggleDebug;

            debugButton.Hover = Audio.OnMenuHover;
            debugButton.Click = Audio.MenuForward;
            backButton.Hover = Audio.OnMenuHover;
            backButton.Click = Audio.MenuBack;
        }

        public void Update(GameTime gameTime)
        {
            debugButton.Update(gameTime);
            backButton.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(GameState.Backdrop, GraphicsManager.Viewport.Bounds, Color.White);
            debugButton.Draw(spriteBatch);
            backButton.Draw(spriteBatch);
            screenTitle.Draw(spriteBatch);
            spriteBatch.End();
        }

        public Button AddButton(string title, int x, int y)
        {
            return new Button(title, x, y, Textures.Empty);
        }

        public Button AddButtonCenter(string title, int x, int y)
        {
            Button button = AddButton(title, x, y);
            GraphicsManager.CenterGameObjectX(button);
            return button;
        }


    }

}
