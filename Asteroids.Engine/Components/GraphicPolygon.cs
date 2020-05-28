using System.Collections.Generic;
using System.Drawing;
using Asteroids.Engine.Enums;
using Asteroids.Engine.Interfaces;

namespace Asteroids.Engine.Components
{
    public class GraphicPolygon : IGraphicPolygon
    {
        public DrawColor Color { get; set; }

        public IList<Point> Points { get; set; } = new List<Point>();
    }
}
