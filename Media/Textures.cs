using Microsoft.Xna.Framework.Graphics;

using StrategyGame.Managers;

namespace StrategyGame.Media
{
    public static class Textures
    {
        public static Texture2D Transparent { get { return ContentService.Instance.Textures["Empty"]; } }
        public static Texture2D Black { get { return ContentService.Instance.Textures["Black"]; } }
        public static Texture2D TestPink { get { return ContentService.Instance.Textures["Test"]; } }
        public static Texture2D TestRed { get { return ContentService.Instance.Textures["TestRed"]; } }
        public static Texture2D Logo { get { return ContentService.Instance.Textures["Logo"]; } }
        public static Texture2D Mine { get { return ContentService.Instance.Textures["Mine"]; } }
        public static Texture2D WorldMap { get { return ContentService.Instance.Textures["WorldMap"]; } }
        public static Texture2D WorldMap2 { get { return ContentService.Instance.Textures["WorldMap2"]; } }
        public static Texture2D DPad { get { return ContentService.Instance.Textures["DPad"]; } }

    }
}
