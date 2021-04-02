using System;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

using StrategyGame.Managers;

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
                MediaPlayer.Stop();
        }

        public static void GameMusic(object source, EventArgs args)
        {
            if (GameState.GameIsActive)
            {
                ToggleMusic(source, args);
                MediaPlayer.Play(MainTheme, GameState.GameTrackPosition);
                MediaPlayer.IsRepeating = true;
            }
            else
            {
                GameState.GameTrackPosition = MediaPlayer.PlayPosition;
                MediaPlayer.Stop();
            }
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
