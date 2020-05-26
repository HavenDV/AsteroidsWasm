using Asteroids.Standard.Enums;
using Asteroids.Standard.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Brushes = Avalonia.Media.Brushes;
using Color = System.Drawing.Color;
using Point = Avalonia.Point;

namespace Asteroids.Avalonia.Controls
{
    public class GraphicContainer : Canvas, IGraphicContainer
    {
        #region Properties

        // Pen is IDisposable
        private IReadOnlyDictionary<DrawColor, Color> ColorCache { get; set; }
        private IEnumerable<IGraphicLine> LastLines { get; set; } = new List<IGraphicLine>();
        private IEnumerable<IGraphicPolygon> LastPolygons { get; set; } = new List<IGraphicPolygon>();

        #endregion

        #region Methods

        public Task Initialize(IReadOnlyDictionary<DrawColor, string> drawColorMap)
        {
            ColorCache = new ReadOnlyDictionary<DrawColor, Color>(
                drawColorMap.ToDictionary(
                    kvp => kvp.Key
                    , kvp => (Color)(new ColorConverter().ConvertFromString(kvp.Value) ?? Colors.White)
                )
            );

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
                    new ImmutablePen(Brushes.AliceBlue), 
                    new Point(line.Point1.X, line.Point1.Y), 
                    new Point(line.Point2.X, line.Point2.Y));
            }
            foreach (var poly in LastPolygons)
            {
                context.DrawGeometry(
                    Brushes.AliceBlue,
                    new ImmutablePen(Brushes.AliceBlue),
                    new PolylineGeometry(poly.Points.Select(i => new Point(i.X, i.Y)), true));
            }
        }

        #endregion
    }
}
