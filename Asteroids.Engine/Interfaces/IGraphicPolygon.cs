using System.Collections.Generic;
using System.Drawing;
using Asteroids.Engine.Enums;

namespace Asteroids.Engine.Interfaces
{
    /// <summary>
    /// Polygon vector to render in the UI.
    /// </summary>
    public interface IGraphicPolygon
    {
        /// <summary>
        /// <see cref="DrawColor"/> for the graphic.
        /// </summary>
        DrawColor Color { get; }

        /// <summary>
        /// Collection of points to connect (non-closing)
        /// </summary>
        IList<Point> Points { get; }
    }
}
