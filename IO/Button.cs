using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using StrategyGame.Managers;
using System;

namespace StrategyGame.IO
{
    public class Button : GameObject
    {
        public Label Label { get; set; }
        public SoundEffect OnHover { get; set; }
        public SoundEffect OnClick { get; set; }

        public event EventHandler<EventArgs> ButtonPressed;

        public Button(string text, int x, int y, Texture2D texture)
        {
            Label = new Label(text, new Vector2(x, y));
            Bounds = new Rectangle(new Point(x, y), GraphicsManager.GetLabelDimensions(Label));
            Texture = texture;
        }

        public void OnHoverSound()
        {
            if (Configuration.WindowInFocus)
            {
                if (OnHover != null)
                    OnHover.Play();
            }
        }

        public virtual void OnButtonPressed()
        {
            if (Configuration.WindowInFocus)
            {
                if (ButtonPressed != null)
                    ButtonPressed(this, EventArgs.Empty);
                if (OnClick != null)
                    OnClick.Play();
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            if (Texture == null)
            {
                Texture = new Texture2D(sb.GraphicsDevice, Bounds.Width, Bounds.Height);
            }
            sb.Draw(Texture, Bounds, Color.White);
            if (Label != null)
            {
                Label.Draw(sb);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Bounds.Contains(Input.CurrentMousePosition))
            {
                Label.Color = Color.Blue;

                if (!Bounds.Contains(Input.PreviousMousePosition))
                {
                    OnHoverSound();
                }
                if (Input.LeftButtonClicked())
                {
                    OnButtonPressed();
                }
            }
                
            else
            {
                Label.Color = Color.White;
            }
                
        }
    }
}
