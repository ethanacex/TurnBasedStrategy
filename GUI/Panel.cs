using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Managers;

namespace StrategyGame.GUI
{
    class Panel : GameObject
    {
        public int BorderWidth { get; private set; }
        private Color borderColor;
        public Panel(Point location, Point size)
        {
            Bounds = new Rectangle(location, size);
            borderColor = Color.Transparent;
            BorderWidth = 0;
        }

        public void SetBorder(int width, Color color)
        {
            BorderWidth = width;
            borderColor = color;
        }

        public override void Draw(SpriteBatch sb)
        {

            if (Texture == null)
                sb.Draw(GameState.Backdrop, Bounds, Configuration.TextureColor);
            else
                sb.Draw(Texture, Bounds, Color.White);

            // Draw the panel bounding box
            GraphicsManager.DrawGameObjectBorder(sb, this, BorderWidth, borderColor);
        }

        public override void Update(GameTime gameTime)
        {
            // Update all items on panel

        }
    }
}
