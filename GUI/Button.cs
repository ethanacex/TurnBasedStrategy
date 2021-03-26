﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using StrategyGame.Managers;
using System;
using StrategyGame.Core;

namespace StrategyGame.GUI
{
    public class Button : GameObject
    {
        private Color defaultTextColor;
        public Label Label { get; set; }
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
        }

        public Button(string text, int x, int y, SpriteFont font)
        {
            Label = new Label(text, new Vector2(x, y), font, Settings.TextColor);
            Bounds = new Rectangle(new Point(x, y), GraphicsManager.GetLabelDimensions(Label));
            Texture = GraphicsManager.GetTextureOfColor(Settings.BackdropColor);
            DefaultLabelColor = Settings.TextColor;
            ToggleHighlight = false;
        }

        // This constructor of Button is used as an invisible clickable overlay area for a pre-rendered UI button texture
        public Button(int x, int y)
        {
            Bounds = new Rectangle(new Point(x, y), new Point(90, 90));
            Texture = GraphicsManager.GetTextureOfColor(Color.Transparent);
            defaultTextColor = Settings.TextColor;
            ToggleHighlight = false;
        }

        private void HoverSound()
        {
            if (GameState.WindowInFocus && GameState.ToggleSFX)
            {
                if (Hover != null)
                    Hover.Play();
            }
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
                    OnButtonPressed();
            }
            else
            {
                if (Label != null)
                    Label.Color = DefaultLabelColor;
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
    }
}
