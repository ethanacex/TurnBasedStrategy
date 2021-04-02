using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Managers;

namespace StrategyGame.Core
{
    public class Panel : GameObject
    {
        public int BorderWidth { get; private set; }
        protected Color borderColor;

        public Panel(Point location, Point size)
        {
            Bounds = new Rectangle(location, size);
            borderColor = Settings.BorderColor;
            BorderWidth = Settings.BorderWidth;
        }

        public void SetCustomBorder(int width, Color color)
        {
            BorderWidth = width;
            borderColor = color;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Begin();
            if (Texture == null)
                sb.Draw(GraphicsManager.GetTextureOfColor(Settings.BackdropColor), Bounds, Settings.TextureColor);
            else
                sb.Draw(Texture, Bounds, Color.White);
            sb.End();
            // Draw the panel bounding box
            GraphicsManager.DrawGameObjectBorder(sb, this, BorderWidth, borderColor);
            
        }

        public override void Update(GameTime gameTime)
        {
            // Update all items on panel

        }
    }
}
