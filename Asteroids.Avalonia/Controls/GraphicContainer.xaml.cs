using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asteroids.Engine.Enums;
using Asteroids.Engine.Interfaces;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Point = Avalonia.Point;

namespace Asteroids.Avalonia.Controls
{
    public class GraphicContainer : Canvas, IGraphicContainer
    {
        #region Properties

        // Pen is IDisposable
        private IReadOnlyDictionary<DrawColor, ImmutableSolidColorBrush>? ColorCache { get; set; }
        private IEnumerable<IGraphicLine> LastLines { get; set; } = new List<IGraphicLine>();
        private IEnumerable<IGraphicPolygon> LastPolygons { get; set; } = new List<IGraphicPolygon>();

        #endregion

        #region Methods

        public Task Initialize(IReadOnlyDictionary<DrawColor, System.Drawing.Color> drawColorMap)
        {
            ColorCache = drawColorMap
                .ToDictionary(
                    pair => pair.Key,
                    pair => new ImmutableSolidColorBrush(new Color(pair.Value.A, pair.Value.R, pair.Value.G, pair.Value.B)));

            return Task.CompletedTask;
        }

        public Task Draw(IEnumerable<IGraphicLine> lines, IEnumerable<IGraphicPolygon> polygons)
        {
            InvalidateVisual();
            LastLines = lines;
            LastPolygons = polygons;

            return Task.CompletedTask;
        }

        #endregion

        #region Overrides

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            if (ColorCache == null)
            {
                return;
            }

            foreach (var line in LastLines)
            {
                context.DrawLine(
                    new ImmutablePen(ColorCache[line.Color]), 
                    new Point(line.Point1.X, line.Point1.Y), 
                    new Point(line.Point2.X, line.Point2.Y));
            }
            foreach (var polygon in LastPolygons)
            {
                context.DrawGeometry(
                    ColorCache[polygon.Color],
                    new ImmutablePen(ColorCache[polygon.Color]),
                    new PolylineGeometry(polygon.Points.Select(i => new Point(i.X, i.Y)), true));
            }
        }

        #endregion
    }
}
