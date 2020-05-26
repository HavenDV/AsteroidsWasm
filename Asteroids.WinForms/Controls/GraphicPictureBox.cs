using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Asteroids.Standard.Enums;
using Asteroids.Standard.Interfaces;

namespace Asteroids.WinForms.Controls
{
    public class GraphicPictureBox : PictureBox, IGraphicContainer
    {
        #region Properties

        // Pen is IDisposable
        private IReadOnlyDictionary<DrawColor, Pen>? ColorCache { get; set; }
        private IEnumerable<IGraphicLine> LastLines { get; set; } = new List<IGraphicLine>();
        private IEnumerable<IGraphicPolygon> LastPolygons { get; set; } = new List<IGraphicPolygon>();

        #endregion

        #region Methods

        public Task Initialize(IReadOnlyDictionary<DrawColor, Color> drawColorMap)
        {
            ColorCache = drawColorMap.ToDictionary(
                pair => pair.Key, 
                pair => new Pen(pair.Value)
            );

            return Task.CompletedTask;
        }

        public Task Draw(IEnumerable<IGraphicLine> lines, IEnumerable<IGraphicPolygon> polygons)
        {
            Invalidate();
            LastLines = lines;
            LastPolygons = polygons;

            return Task.CompletedTask;

        }

        #endregion

        #region Overrides

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (ColorCache == null)
            {
                return;
            }

            foreach (var line in LastLines)
            {
                e.Graphics.DrawLine(ColorCache[line.Color], line.Point1, line.Point2);
            }
            foreach (var poly in LastPolygons)
            {
                e.Graphics.DrawPolygon(ColorCache[poly.Color], poly.Points.ToArray());
            }
        }

        #endregion
    }
}
