using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.GUI;
using StrategyGame.Managers;
using StrategyGame.Media;

namespace StrategyGame.Core
{
    public class MapNav : GameObject
    {
        private Texture2D navigation;
        private const int clickArea = 90;

        public Button UpBtn { get; private set; }
        public Button DownBtn { get; private set; }
        public Button LeftBtn { get; private set; }
        public Button RightBtn { get; private set; }

        public void Initialize(Point position)
        {
            navigation = Textures.DPad;
            Bounds = new Rectangle(position, navigation.Bounds.Size);
            UpBtn = new Button(Bounds.Center.X - (clickArea / 2), Bounds.Top + 10);
            DownBtn = new Button(Bounds.Center.X - (clickArea / 2), Bounds.Bottom - (clickArea + 10));
            LeftBtn = new Button(Bounds.Left + 30, Bounds.Center.Y - (clickArea / 2));
            RightBtn = new Button(Bounds.Right - (clickArea + 30), Bounds.Center.Y - (clickArea / 2));

            UpBtn.Click = Audio.Navigate;
            DownBtn.Click = Audio.Navigate;
            LeftBtn.Click = Audio.Navigate;
            RightBtn.Click = Audio.Navigate;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(navigation, Bounds, Settings.TextureColor);
            UpBtn.Draw(sb);
            DownBtn.Draw(sb);
            LeftBtn.Draw(sb);
            RightBtn.Draw(sb);

            if (GameState.DebugMode)
            {
                GraphicsManager.DrawGameObjectBorder(sb, UpBtn, 3, Color.Red);
                GraphicsManager.DrawGameObjectBorder(sb, DownBtn, 3, Color.Red);
                GraphicsManager.DrawGameObjectBorder(sb, LeftBtn, 3, Color.Red);
                GraphicsManager.DrawGameObjectBorder(sb, RightBtn, 3, Color.Red);
            }
        }

        public override void Update(GameTime gameTime)
        {
            UpBtn.Update(gameTime);
            DownBtn.Update(gameTime);
            LeftBtn.Update(gameTime);
            RightBtn.Update(gameTime);
        }
    }
}
