﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using StrategyGame.Managers;

namespace StrategyGame.Media
{
    public class Fonts
    {
        public static SpriteFont MenuFont { get; } = ContentService.Instance.Fonts["MenuFont"];
        public static SpriteFont InGameFont { get; } = ContentService.Instance.Fonts["InGameFont"];

    }
}
