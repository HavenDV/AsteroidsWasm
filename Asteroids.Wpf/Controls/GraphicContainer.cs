using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Asteroids.Standard.Enums;
using Asteroids.Standard.Interfaces;

namespace Asteroids.Wpf.Controls
{
    /// <summary>
    /// Control to paint vectors based on <see cref="WriteableBitmap"/>.
    /// </summary>
    public class GraphicContainer : Image, IGraphicContainer, IDisposable
    {
        #region Properties

        private Dispatcher MainDispatcher { get; } = Dispatcher.CurrentDispatcher;
        private IReadOnlyDictionary<DrawColor, Color>? ColorCache { get; set; }
        private WriteableBitmap? Bitmap { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="GraphicContainer"/>.
        /// </summary>
        public GraphicContainer()
        {
            SizeChanged += OnSizeChanged;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize the <see cref="WriteableBitmap"/> with the current width and height.
        /// </summary>
        public Task Initialize(IReadOnlyDictionary<DrawColor, System.Drawing.Color> drawColorMap)
        {
            ColorCache = drawColorMap
                .ToDictionary(
                    pair => pair.Key, 
                    pair => Color.FromArgb(pair.Value.A, pair.Value.R, pair.Value.G, pair.Value.B));

            //Since the control has no size yet simply draw a size bitmap
            Bitmap = BitmapFactory.New(0, 0);
            Source = Bitmap;

            return Task.CompletedTask;
        }

        /// <summary>
        /// Draws the collection of <see cref="IGraphicLine"/>s and <see cref="IGraphicPolygon"/>s
        /// to the screen.
        /// </summary>
        public async Task Draw(IEnumerable<IGraphicLine> lines, IEnumerable<IGraphicPolygon> polygons)
        {
            ColorCache = ColorCache ?? throw new InvalidOperationException("ColorCache is null");

            try
            {
                await MainDispatcher.InvokeAsync(() =>
                {
                    Bitmap.Clear();

                    foreach (var line in lines)
                    {
                        Bitmap.DrawLine(
                            line.Point1.X,
                            line.Point1.Y,
                            line.Point2.X,
                            line.Point2.Y, 
                            ColorCache[line.Color]
                        );
                    }

                    foreach (var polygon in polygons)
                    {
                        var points = new int[polygon.Points.Count * 2 + 2];

                        for (int i = 0, c = 0; i < points.Length - 2; i += 2, c++)
                        {
                            var point = polygon.Points[c];

                            points[i] = point.X;
                            points[i + 1] = point.Y;
                        }

                        var first = polygon.Points.First();
                        points[points.Length - 2] = first.X;
                        points[points.Length - 1] = first.Y;

                        Bitmap.DrawPolyline(points, ColorCache[polygon.Color]);
                    }
                });
            }
            catch (Exception)
            {
                //Ignore
            }
        }

        /// <summary>
        /// Cleanup handlers.
        /// </summary>
        public void Dispose()
        {
            SizeChanged -= OnSizeChanged;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Resize the <see cref="WriteableBitmap"/> based on new control size.
        /// </summary>
        private void OnSizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            //Resize the current bitmap
            Bitmap = Bitmap.Resize(
                (int)e.NewSize.Width,
                (int)e.NewSize.Height,
                WriteableBitmapExtensions.Interpolation.Bilinear
            );

            Source = Bitmap;
        }


        #endregion
    }
}
