using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using TurnBasedStrategy.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace TurnBasedStrategy.Media
{
    public class Sounds
    {
        public static SoundEffect OnMenuHover { get; } = ContentService.Instance.SFX["MenuHover"];
        public static SoundEffect MenuForward { get; } = ContentService.Instance.SFX["MenuForward"];
        public static SoundEffect MenuBack { get; } = ContentService.Instance.SFX["MenuBack"];

    }
}
