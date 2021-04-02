using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

using StrategyGame.Managers;
using StrategyGame.Core;
using StrategyGame.Media;

namespace StrategyGame.GUI
{
    public class Button : GameObject
    {
        private Color defaultTextColor;
        private bool clicked;
        public Label Label { get; set; }
        public Effect ClickEffect { get; set; }
        public SoundEffect Hover { get; set; }
        public SoundEffect Click { get; set; }
        public Color DefaultLabelColor { get { return defaultTextColor; } set { defaultTextColor = value; Label.Color = value; } }
        public bool ToggleHighlight { get; set; }

        public event EventHandler<EventArgs> ButtonPressed;

        public Button(string text, int x, int y, Texture2D texture)
        {
            Label = new Label(text, new Vector2(x, y));
            Bounds = new Rectangle(new Point(x, y), GraphicsManager.GetLabelDimensions(Label));
            Texture = texture;
            defaultTextColor = Label.Color;
            ToggleHighlight = true;
            clicked = false;
        }

        public Button(string text, int x, int y, SpriteFont font)
        {
            Label = new Label(text, new Vector2(x, y), font, Settings.TextColor);
            Bounds = new Rectangle(new Point(x, y), GraphicsManager.GetLabelDimensions(Label));
            Texture = GraphicsManager.GetTextureOfColor(Settings.BackdropColor);
            DefaultLabelColor = Settings.TextColor;
            ToggleHighlight = false;
            clicked = false;
        }

        // This constructor of Button is used as an invisible clickable overlay area for a pre-rendered UI button texture
        public Button(int x, int y)
        {
            Bounds = new Rectangle(new Point(x, y), new Point(90, 90));
            Texture = Textures.Transparent;
            defaultTextColor = Settings.TextColor;
            ToggleHighlight = false;
            clicked = false;
        }

        private void HoverSound()
        {
            if (GameState.WindowInFocus && GameState.ToggleSFX)
            {
                if (Hover != null)
                    Hover.Play();
            }
        }

        // Programmatically press this button
        // This could later allow the game to be played without a mouse
        public void Press()
        {
            OnButtonPressed();
            clicked = true;
        }

        private void OnButtonPressed()
        {
            if (GameState.WindowInFocus)
            {
                if (ButtonPressed != null)
                    ButtonPressed(this, EventArgs.Empty);
                if (Click != null && GameState.ToggleSFX)
                    Click.Play();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Bounds.Contains(Input.CurrentMousePosition))
            {
                if (ToggleHighlight)
                    if (Label != null)
                        Label.Color = Color.Blue;

                if (!Bounds.Contains(Input.PreviousMousePosition))
                    HoverSound();

                if (Input.LeftButtonClicked())
                {
                    OnButtonPressed();
                    clicked = true;
                }
            }
            else
            {
                if (Label != null)
                    Label.Color = DefaultLabelColor;
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Begin(SpriteSortMode.Immediate, BlendState.Additive);

            if (clicked && ClickEffect != null)
                ClickEffect.CurrentTechnique.Passes[0].Apply();

            // Discard clicked value only once it has been used
            clicked = false;

            if (Texture == null)
                Texture = new Texture2D(sb.GraphicsDevice, Bounds.Width, Bounds.Height);

            sb.Draw(Texture, Bounds, Color.White);
            sb.End();

            if (Label != null)
                Label.Draw(sb);
        }
    }
}
