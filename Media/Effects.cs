using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Managers;

namespace StrategyGame.Media
{
    public static class Effects
    {
        public static Effect DPadClick { get; } = ContentService.Instance.Effects["DPadClick"];

    }
}
