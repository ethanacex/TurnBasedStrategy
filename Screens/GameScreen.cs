using TurnBasedStrategy.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TurnBasedStrategy;
using TurnBasedStrategy.Media;

namespace TurnBasedStrategy.Screens
{
    class GameScreen : IScreen
    {
        ScreenManager screenManager;
        Grid grid;

        public void Initialize(ScreenManager screenManager)
        {
            this.screenManager = screenManager;
            grid = new Grid(Configuration.GridWidth, Configuration.GridHeight, Scene.Empty);
        }

        public void Update(GameTime gameTime)
        {
            grid.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            grid.Draw(spriteBatch);
            spriteBatch.End();
        }

    }
}
