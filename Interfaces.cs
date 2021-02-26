using TurnBasedStrategy.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TurnBasedStrategy.Managers;

namespace TurnBasedStrategy.Screens
{
    interface IScreen
    {
        void Initialize(ScreenManager screenManager);
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime);
    }
}
