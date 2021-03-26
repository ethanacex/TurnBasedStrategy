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
        public static SoundEffect Navigate { get; } = ContentService.Instance.SFX["NavSound"];

        public static Song MenuTheme { get; } = ContentService.Instance.Music["MenuTheme"];
        public static Song MainTheme { get; } = ContentService.Instance.Music["MainTheme"];

        public static void MenuMusic(object source, EventArgs args)
        {
            if (GameState.MenuIsActive)
            {
                ToggleMusic(source, args);
                MediaPlayer.Play(MenuTheme);
                MediaPlayer.IsRepeating = true;
            }
            else
                MediaPlayer.Pause();
        }

        public static void GameMusic(object source, EventArgs args)
        {
            if (!GameState.MenuIsActive)
            {
                ToggleMusic(source, args);
                MediaPlayer.Play(MainTheme);
                MediaPlayer.IsRepeating = true;
            }
            else
                MediaPlayer.Pause();
        }

        public static void ToggleMusic(object source, EventArgs args)
        {
            if (GameState.ToggleMusic)
                MediaPlayer.IsMuted = false;
            else
                MediaPlayer.IsMuted = true;
        }

    }
}
