using System.Drawing;
using Asteroids.Engine.Enums;
using Asteroids.Engine.Interfaces;

namespace Asteroids.Engine.Components
{
    public class GraphicLine : IGraphicLine
    {
        public DrawColor Color { get; set; }

        public Point Point1 { get; set; }

        public Point Point2 { get; set; }
    }
}
