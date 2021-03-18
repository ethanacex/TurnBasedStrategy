using StrategyGame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace StrategyGame.Screens
{
    interface IScreen
    {
        void Initialize(ScreenManager screenManager);
        void Reinitialize(object sender, EventArgs e);
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime);
    }
}
