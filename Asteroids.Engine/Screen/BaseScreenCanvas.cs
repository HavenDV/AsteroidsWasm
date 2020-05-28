using System;
using System.Drawing;

namespace Asteroids.Engine.Screen
{

    /// <summary>
    /// Drawing canvas to which all heights and widths will be scaled.
    /// </summary>
    /// <remarks>
    /// Angle 0 is pointing "down", 90 is "left" on the canvas
    /// </remarks>
    public class BaseScreenCanvas
    {
        protected readonly object _updatePointsLock;
        protected readonly object _updatePolysLock;

        protected Point _lastPoint;

        /// <summary>
        /// Creates a new instance of <see cref="BaseScreenCanvas"/>.
        /// </summary>
        /// <param name="size">Initial actual size.</param>
        public BaseScreenCanvas(Rectangle size)
        {
            Size = size;

            _updatePointsLock = new object();
            _updatePolysLock = new object();

            //Set in case a call to add to end is made prior to creating a line
            _lastPoint = new Point(0, 0);
        }

        /// <summary>
        /// Current ACTUAL size of the canvas.
        /// </summary>
        public Rectangle Size { get; set; }

        #region Statics

        /// <summary>
        /// Refresh rate.
        /// </summary>
        public const double FramesPerSecond = 60;

        /// <summary>
        /// Conversion from degrees to radians.
        /// </summary>
        public const double RadiansPerDegree = Math.PI / 180;

        /// <summary>
        /// Amount of radians in a full circle (i.e. 360 degrees)
        /// </summary>
        public const double RadiansPerCircle = Math.PI * 2;

        /// <summary>
        /// Horizontal width (effective) of the drawing plane.
        /// </summary>
        /// <remarks>
        /// All points and polygons will be drawn using this value and then 
        /// translated to the actual value set by <see cref="Size"/>.
        /// </remarks>
        public const int CanvasWidth = 10000;

        /// <summary>
        /// <see cref="CanvasWidth"/> as <see langword="double"/> to avoid casting.
        /// </summary>
        public const double CanvasWidthDouble = CanvasWidth;

        /// <summary>
        /// Vertical height (effective) of the drawing plane.
        /// </summary>
        /// <remarks>
        /// All points and polygons will be drawn using this value and then 
        /// translated to the actual value set by <see cref="Size"/>.
        /// </remarks>
        public const int CanvasHeight = 7500;

        /// <summary>
        /// <see cref="CanvasHeight"/> as <see langword="double"/> to avoid casting.
        /// </summary>
        public const double CanvasHeightDouble = CanvasHeight;

        /// <summary>
        /// Default explosion time factor relative to the <see cref="FramesPerSecond"/>.
        /// </summary>
        public const int DefaultExplosionLength = 1;

        #endregion
    }
}
