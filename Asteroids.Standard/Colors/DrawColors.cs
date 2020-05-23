﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Asteroids.Standard.Enums;
using Asteroids.Standard.Helpers;

namespace Asteroids.Standard.Colors
{
    /// <summary>
    /// Drawing colors used by the game engine.
    /// </summary>
    internal static class DrawColors
    {
        /// <summary>
        /// Collection of <see cref="DrawColor"/> HEX string values used by the game engine.
        /// </summary>
        public static IReadOnlyDictionary<DrawColor, string> DrawColorMap { get; } = new Dictionary<DrawColor, string>
        {
            [DrawColor.White] = Color.White.ToHexString(),
            [DrawColor.Red] = Color.Red.ToHexString(),
            [DrawColor.Yellow] = Color.Yellow.ToHexString(),
            [DrawColor.Orange] = Color.Orange.ToHexString(),
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
