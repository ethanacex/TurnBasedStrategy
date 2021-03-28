using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.GUI;
using StrategyGame.Managers;
using StrategyGame.Media;
using StrategyGame.Screens;
using System;

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

        public event EventHandler<EventArgs> UpPressed;
        public event EventHandler<EventArgs> DownPressed;
        public event EventHandler<EventArgs> LeftPressed;
        public event EventHandler<EventArgs> RightPressed;

        public void Initialize(Point position)
        {
            navigation = Textures.DPad;
            Bounds = new Rectangle(position, navigation.Bounds.Size);
            UpBtn = new Button(Bounds.Center.X - (clickArea / 2), Bounds.Top);
            DownBtn = new Button(Bounds.Center.X - (clickArea / 2), Bounds.Bottom - (clickArea));
            LeftBtn = new Button(Bounds.Left + 35, Bounds.Center.Y - (clickArea / 2));
            RightBtn = new Button(Bounds.Right - (clickArea + 35), Bounds.Center.Y - (clickArea / 2));

            UpBtn.Click = Audio.Navigate;
            DownBtn.Click = Audio.Navigate;
            LeftBtn.Click = Audio.Navigate;
            RightBtn.Click = Audio.Navigate;

            UpBtn.ClickEffect = Effects.DPadClick;
            DownBtn.ClickEffect = Effects.DPadClick;
            LeftBtn.ClickEffect = Effects.DPadClick;
            RightBtn.ClickEffect = Effects.DPadClick;

            UpBtn.ButtonPressed += NavigateUp;
            DownBtn.ButtonPressed += NavigateDown;
            LeftBtn.ButtonPressed += NavigateLeft;
            RightBtn.ButtonPressed += NavigateRight;
        }

        private void OnUpPressed()
        {
            if (UpPressed != null)
                UpPressed(this, EventArgs.Empty);
        }

        private void OnDownPressed()
        {
            if (DownPressed != null)
                DownPressed(this, EventArgs.Empty);
        }

        private void OnLeftPressed()
        {
            if (LeftPressed != null)
                LeftPressed(this, EventArgs.Empty);
        }

        private void OnRightPressed()
        {
            if (RightPressed != null)
                RightPressed(this, EventArgs.Empty);
        }

        private void NavigateRight(object sender, EventArgs e)
        {
            int x = Settings.GridPosition.X;
            x -= (2 * Settings.TileWidth);
            Settings.GridPosition = new Point(x, Settings.GridPosition.Y);
            OnRightPressed();
        }

        private void NavigateLeft(object sender, EventArgs e)
        {
            int x = Settings.GridPosition.X;
            x += (2 * Settings.TileWidth);
            Settings.GridPosition = new Point(x, Settings.GridPosition.Y);
            OnLeftPressed();
        }

        private void NavigateDown(object sender, EventArgs e)
        {
            int y = Settings.GridPosition.Y;
            y -= (2 * Settings.TileHeight);
            Settings.GridPosition = new Point(Settings.GridPosition.X, y);
            OnDownPressed();
        }

        private void NavigateUp(object sender, EventArgs e)
        {
            int y = Settings.GridPosition.Y;
            y += (2 * Settings.TileHeight);
            Settings.GridPosition = new Point(Settings.GridPosition.X, y);
            OnUpPressed();
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(navigation, Bounds, Settings.TextureColor);
            sb.End();

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
