using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.Media
{
    public static class Textures
    {
        public static Texture2D Empty { get; } = ContentService.Instance.Textures["EmptyBlack"];
        public static Texture2D TestPink { get; } = ContentService.Instance.Textures["Test"];
        public static Texture2D TestRed { get; } = ContentService.Instance.Textures["TestRed"];
        public static Texture2D Logo { get; } = ContentService.Instance.Textures["Logo"];
        public static Texture2D Mine { get; } = ContentService.Instance.Textures["Mine"];
    }
}
