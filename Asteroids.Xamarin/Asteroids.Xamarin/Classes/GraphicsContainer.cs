using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asteroids.Standard.Enums;
using Asteroids.Standard.Interfaces;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Color = System.Drawing.Color;

namespace Asteroids.Xamarin.Classes
{
    public class GraphicsContainer : SKCanvasView, IGraphicContainer, IRegisterable
    {
        private IReadOnlyDictionary<DrawColor, SKColor> _colorCache;
        private IEnumerable<IGraphicLine> _lastLines = new List<IGraphicLine>();
        private IEnumerable<IGraphicPolygon> _lastPolygons = new List<IGraphicPolygon>();


        public Task Initialize(IReadOnlyDictionary<DrawColor, Color> drawColorMap)
        {
            _colorCache = drawColorMap
                .ToDictionary(
                    pair => pair.Key,
                    pair => ColorToSkColor(pair.Value));

            PaintSurface += OnPaintSurface;
            return Task.CompletedTask;
        }

        public Task Draw(IEnumerable<IGraphicLine> lines, IEnumerable<IGraphicPolygon> polygons)
        {
            _lastLines = lines;
            _lastPolygons = polygons;

            InvalidateSurface();

            return Task.CompletedTask;
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear();

            foreach (var gline in _lastLines)
            {
                var paint = new SKPaint
                {
                    Color = _colorCache[gline.Color],
                    IsStroke = true,
                };

                var p0 = new SKPoint(gline.Point1.X, gline.Point1.Y);
                var p1 = new SKPoint(gline.Point2.X, gline.Point2.Y);

                canvas.DrawLine(p0, p1, paint);
            }

            foreach (var gpoly in _lastPolygons)
            {
                   var paint = new SKPaint
                {
                    Color = _colorCache[gpoly.Color],
                    IsStroke = true,
                };

                var path = new SKPath();
                path.AddPoly(gpoly.Points.Select(p => new SKPoint(p.X, p.Y)).ToArray());

                canvas.DrawPath(path, paint);
            }
        }

        #region Color

        private static SKColor ColorToSkColor(Color color)
        {
            return new SKColor(color.R, color.G, color.B, color.A);
        }

        #endregion
    }
}
