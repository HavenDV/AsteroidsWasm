using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Asteroids.Engine.Enums;

namespace Asteroids.Engine.Colors
{
    /// <summary>
    /// Drawing colors used by the game engine.
    /// </summary>
    public static class DrawColors
    {
        /// <summary>
        /// Collection of <see cref="DrawColor"/> HEX string values used by the game engine.
        /// </summary>
        public static IReadOnlyDictionary<DrawColor, Color> DrawColorMap { get; } = new Dictionary<DrawColor, Color>
        {
            [DrawColor.White] = Color.White,
            [DrawColor.Red] = Color.Red,
            [DrawColor.Yellow] = Color.Yellow,
            [DrawColor.Orange] = Color.Orange,
        };

        /// <summary>
        /// Collection of <see cref="DrawColor"/> keys in <see cref="DrawColorMap"/>.
        /// </summary>
        public static IReadOnlyList<DrawColor> DrawColorList { get; } = DrawColorMap
            .Keys
            .OrderBy(k => k)
            .ToList();
    }
}
