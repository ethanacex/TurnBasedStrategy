using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Media;
using StrategyGame.IO;
using StrategyGame.Screens;
using System;
using System.Collections.Generic;

namespace StrategyGame.Managers
{
    class OptionsScreen : IScreen
    {
        ScreenManager screenManager;
        Label screenTitle;
        Button debugButton;
        Button backButton;
        Button musicButton;
        Button fullScreenButton;
        Button resolutionButton;
        Button lowResButton;
        Button highDefButton;

        List<Button> optionsMenuButtons;
        List<Button> subMenuButtons;
        private bool inSubmenu;

        public void Initialize(ScreenManager screenManager)
        {
            this.screenManager = screenManager;
            optionsMenuButtons = new List<Button>();
            subMenuButtons = new List<Button>();
            inSubmenu = false;

            Rectangle view = GraphicsManager.Viewport.Bounds;
            Point screenCenter = GraphicsManager.Viewport.Bounds.Center;
            
            resolutionButton = CreateCenterAlignedButton("Change Resolution", screenCenter.X, screenCenter.Y - 100);
            musicButton = CreateCenterAlignedButton("Toggle Music", screenCenter.X, screenCenter.Y - 25);
            debugButton = CreateCenterAlignedButton("Toggle Debug", screenCenter.X, screenCenter.Y + 50);

            lowResButton = CreateCenterAlignedButton("1280 x 720", screenCenter.X, screenCenter.Y - 100);
            highDefButton = CreateCenterAlignedButton("1920 x 1080", screenCenter.X, screenCenter.Y - 25);
            fullScreenButton = CreateCenterAlignedButton("Toggle Fullscreen", screenCenter.X, screenCenter.Y + 50);

            backButton = CreateCenterAlignedButton("Back", screenCenter.X, debugButton.Y + 125);
            screenTitle = AddLabelCenter("Options Menu", screenCenter.X, resolutionButton.Y - 175);

            optionsMenuButtons.Add(resolutionButton);
            optionsMenuButtons.Add(musicButton);
            optionsMenuButtons.Add(debugButton);

            subMenuButtons.Add(fullScreenButton);
            subMenuButtons.Add(lowResButton);
            subMenuButtons.Add(highDefButton);

            // Assign Event Subscribers
            backButton.ButtonPressed += PreviousScreen;
            debugButton.ButtonPressed += screenManager.ToggleDebug;
            musicButton.ButtonPressed += screenManager.ToggleMusic;
            resolutionButton.ButtonPressed += NavigateSubMenu;
            fullScreenButton.ButtonPressed += screenManager.ToggleFullscreen;
            lowResButton.ButtonPressed += screenManager.ToggleLowRes;
            highDefButton.ButtonPressed += screenManager.ToggleHighRes;

            SetMenuButtonAudio(debugButton);
            SetMenuButtonAudio(musicButton);
            SetMenuButtonAudio(resolutionButton);
            SetMenuButtonAudio(fullScreenButton);
            SetMenuButtonAudio(lowResButton);
            SetMenuButtonAudio(highDefButton);

            backButton.Hover = Audio.OnMenuHover;
            backButton.Click = Audio.MenuBack;
        }

        private void PreviousScreen(object sender, EventArgs e)
        {
            if (inSubmenu)
                inSubmenu = !inSubmenu;
            else
                screenManager.PreviousScreen(sender, e);
        }

        private void NavigateSubMenu(object sender, EventArgs e)
        {
            inSubmenu = !inSubmenu;
        }

        public void Update(GameTime gameTime)
        {
            if (inSubmenu)
                foreach (var button in subMenuButtons)
                    button.Update(gameTime);
            else
                foreach (var button in optionsMenuButtons)
                    button.Update(gameTime);
            backButton.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(GraphicsManager.GetTextureOfColor(Settings.BackdropColor), GraphicsManager.Viewport.Bounds, Color.White);
            
            screenTitle.Draw(spriteBatch);
            backButton.Draw(spriteBatch);

            if (inSubmenu)
                foreach (var button in subMenuButtons)
                    button.Draw(spriteBatch);
            else
                foreach (var button in optionsMenuButtons)
                    button.Draw(spriteBatch);

            spriteBatch.End();
        }

        public Button CreateButton(string title, int x, int y)
        {
            return new Button(title, x, y, Textures.Empty);
        }

        public Button CreateCenterAlignedButton(string title, int x, int y)
        {
            Button button = CreateButton(title, x, y);
            GraphicsManager.CenterGameObjectX(button);
            return button;
        }

        public Label AddLabelCenter(string title, int x, int y)
        {
            Label label = new Label(title, new Vector2(x, y));
            GraphicsManager.CenterGameObjectX(label);
            return label;
        }

        public void SetMenuButtonAudio(Button button)
        {
            button.Hover = Audio.OnMenuHover;
            button.Click = Audio.MenuForward;
        }


    }

}
