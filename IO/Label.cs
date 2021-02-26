using TurnBasedStrategy.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TurnBasedStrategy.Media;

namespace TurnBasedStrategy.IO
{
    public class Label : GameObject
    {
        public SpriteFont Font { get; set; }
        public Color Color { get; set; }
        public string Body { get; set; }

        public Label(string body, Vector2 position)
        {
            Body = body;
            Font = Fonts.FixedSys;
            Color = Configuration.TextColor;
            Texture = Configuration.LabelTexture;

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
            sb.Draw(Texture, Bounds, Configuration.TextureColor);
            sb.DrawString(Font, Body, Bounds.Location.ToVector2(), Color);
        }
    }
}
