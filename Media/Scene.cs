using Microsoft.Xna.Framework.Graphics;
using TurnBasedStrategy.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace TurnBasedStrategy.Media
{
    public static class Scene
    {
        public static Texture2D Empty { get; } = ContentService.Instance.Textures["EmptyBlack"];
        public static Texture2D TestPink { get; } = ContentService.Instance.Textures["Test"];
        public static Texture2D TestRed { get; } = ContentService.Instance.Textures["TestRed"];
    }
}
