using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using StrategyGame.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.Media
{
    public class Audio
    {
        public static SoundEffect OnMenuHover { get; } = ContentService.Instance.SFX["MenuHover"];
        public static SoundEffect MenuForward { get; } = ContentService.Instance.SFX["MenuForward"];
        public static SoundEffect MenuBack { get; } = ContentService.Instance.SFX["MenuBack"];
        public static Song MenuTheme { get; } = ContentService.Instance.Music["MenuTheme"];

        public static void MenuMusic(object source, EventArgs args)
        {
            if (GameState.IsOnMenuScreen)
            {
                MediaPlayer.Play(MenuTheme);
                MediaPlayer.IsRepeating = true;
            }
            else
                MediaPlayer.Stop();
        }

    }
}
