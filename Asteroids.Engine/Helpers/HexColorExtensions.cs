using System.Drawing;

namespace Asteroids.Engine.Helpers
{
    /// <summary>
    /// Helpers for converting HEX-based colors and strings.
    /// </summary>
    public static class HexColorExtensions
    {
        /// <summary>
        /// Converts a <see cref="Color"/> to an html-formatted text string (e.g. #RRGGBB).
        /// </summary>
        public static string ToHex(this Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }
    }
}