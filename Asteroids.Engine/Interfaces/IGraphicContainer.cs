﻿using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Asteroids.Engine.Enums;

namespace Asteroids.Engine.Interfaces
{
    /// <summary>
    /// Main graphic container to draw <see cref="IGraphicLine"/>s and <see cref="IGraphicPolygon"/>s.
    /// </summary>
    public interface IGraphicContainer
    {
        /// <summary>
        /// Initialize the container before painting starts.
        /// </summary>
        /// <param name="drawColorMap">Collection (read-only) of <see cref="DrawColor"/> used by the game engine and associated HEX-based (HTML) color strings.</param>
        Task Initialize(IReadOnlyDictionary<DrawColor, Color> drawColorMap);

        /// <summary>
        /// Paint or repaint the canvas with the collections of lines and polygons (unfilled).
        /// </summary>
        /// <param name="lines">Collection of <see cref="IGraphicLine"/>.</param>
        /// <param name="polygons">Collection of <see cref="IGraphicPolygon"/>.</param>
        Task Draw(IEnumerable<IGraphicLine> lines, IEnumerable<IGraphicPolygon> polygons);
    }
}
