using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.IO;
using StrategyGame.Managers;
using StrategyGame.Media;

namespace StrategyGame.GUI
{
    class Panel : GameObject
    {

        public Panel(Point location, Point size)
        {
            Bounds = new Rectangle(location, size);
            Texture = Configuration.BackdropTexture;
        }

        public override void Draw(SpriteBatch sb)
        {
            // Draw the panel bounding box
            if (GameState.ToggleBorder)
                GraphicsManager.DrawGameObjectBorder(sb, this);

            // Draw all subsequent items on panel
            sb.Draw(Configuration.BackdropTexture, Bounds, Configuration.TextureColor);
        }

        public override void Update(GameTime gameTime)
        {
            // Update all items on panel

        }
    }
}
