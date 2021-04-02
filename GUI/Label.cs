using StrategyGame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Media;
using StrategyGame.Core;

namespace StrategyGame.GUI
{
    public class Label : GameObject
    {
        public SpriteFont Font { get; set; }
        public Color Color { get; set; }
        public string Body { get; set; }

        public Label(string body, Vector2 position)
        {
            Body = body;
            Font = Fonts.MenuFont;
            Color = Settings.TextColor;
            Texture = Textures.Black;

            Bounds = new Rectangle(position.ToPoint(), Font.MeasureString(Body).ToPoint());
        }

        public Label(string body, Vector2 position, SpriteFont font, Color color)
        {
            Body = body;
            Font = font;
            Color = color;
            Texture = Textures.Black;

            Bounds = new Rectangle(position.ToPoint(), Font.MeasureString(Body).ToPoint());
        }

        public Vector2 GetDimensions()
        {
            return Font.MeasureString(Body);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(Texture, Bounds, Settings.TextureColor);
            sb.DrawString(Font, Body, Bounds.Location.ToVector2(), Color);
            sb.End();
        }
    }
}
