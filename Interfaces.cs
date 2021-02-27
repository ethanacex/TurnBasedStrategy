using StrategyGame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StrategyGame.Screens
{
    interface IScreen
    {
        void Initialize(ScreenManager screenManager);
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime);
    }
}
