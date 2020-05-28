using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Asteroids.Engine.Helpers;
using Asteroids.Engine.Screen;

namespace Asteroids.Engine.Base
{
    /// <summary>
    /// ScreenObject - defines an object to be displayed on screen
    /// This object is based on a cartesian coordinate system 
    /// centered at 0, 0
    /// </summary>
    public abstract class BaseScreenObject
    {
        /// <summary>
        /// Creates a new instance of <see cref="BaseScreenObject"/>.
        /// </summary>
        /// <param name="location">Absolute origin (bottom-left) of the object.</param>
        protected BaseScreenObject(Point location)
        {
            //templates are drawn nose "up"
            Radians = 180 * BaseScreenCanvas.RadiansPerDegree;

            CurrLoc = location;
        }

        #region Points for Drawing Object

        /// <summary>
        /// Points is used for the internal cartesian system.
        /// </summary>
        private ConcurrentStack<Point> Points { get; } = new ConcurrentStack<Point>();

        /// <summary>
        /// Points is used for the internal cartesian system with rotation angle applied.
        /// </summary>
        private ConcurrentStack<Point> PointsTransformed { get; } = new ConcurrentStack<Point>(); // exposed to simplify explosions


        /// <summary>
        /// Add points to internal collection used to calculate drawn polygons.
        /// </summary>
        /// <param name="points"><see cref="Point"/> collection to add to internal collections.</param>
        /// <returns>Index of the last point inserted.</returns>
        public int AddPoints(IList<Point> points)
        {
            Points.PushRange(points.ToArray());
            PointsTransformed.PushRange(points.ToArray());

            return PointsTransformed.Count - 1;
        }

        /// <summary>
        /// Returns transformed <see cref="Point"/>s to generate object 
        /// polygon in a thead-safe manner relative to current location on the 
        /// </summary>
        /// <returns>Collection of <see cref="Point"/>s.</returns>
        public IList<Point> GetPoints()
        {
            return PointsTransformed
                .ToArray()
                .Select(point => new Point(point.X + CurrLoc.X, point.Y + CurrLoc.Y))
                .ToArray();
        }

        /// <summary>
        /// Clears all internal and transformed <see cref="Point"/>s used to generate 
        /// polygons in a thead-safe manner.
        /// </summary>
        public void ClearPoints()
        {
            Points.Clear();
            PointsTransformed.Clear();
        }

        #endregion

        #region Rotation

        /// <summary>
        /// Max number of clockwise radians allow for an <see cref="Align"/> call.
        /// </summary>
        protected const double RotationLimit = 5 * BaseScreenCanvas.RadiansPerDegree;

        /// <summary>
        /// Max number of counter-clockwise radians allow for an <see cref="Align"/> call.
        /// </summary>
        protected const double RotationLimitNeg = -5 * BaseScreenCanvas.RadiansPerDegree;

        /// <summary>
        /// Get the current rotational radians.
        /// </summary>
        public double GetRadians() => Radians;

        /// <summary>
        /// Current rotation.
        /// </summary>
        protected double Radians;

        /// <summary>
        /// Rotates all internal <see cref="Point"/>s used to generate polygons on draw
        /// based on the alignment with the target point but no moren then 5 degrees at a time.
        /// </summary>
        /// <param name="alignPoint"><see cref="Point"/> to target.</param>
        protected void Align(Point alignPoint)
        {
            var radsToPoint = GeometryHelper.GetAngle(CurrLoc, alignPoint);
            var delta = radsToPoint - Radians;

            Radians += delta >= 0
                ? Math.Min(delta, RotationLimit)
                : Math.Max(delta, RotationLimitNeg);

            RotateInternal();
        }

        /// <summary>
        /// Rotates all internal <see cref="Point"/>s used to generate polygons on draw
        /// by a number of decimal degrees.
        /// </summary>
        /// <param name="degrees">Rotation amount in degrees.</param>
        protected void Rotate(double degrees)
        {
            //Get radians in 1/FramesPerSecond'th increment
            var radiansAdjust = degrees * BaseScreenCanvas.RadiansPerDegree;
            Radians += radiansAdjust / BaseScreenCanvas.FramesPerSecond;

            RotateInternal();
        }

        /// <summary>
        /// Rotates all internal <see cref="Point"/>s used to generate polygons on draw
        /// based on decimal degrees.
        /// </summary>
        private void RotateInternal()
        {
            Radians %= BaseScreenCanvas.RadiansPerCircle;

            var sinVal = Math.Sin(Radians);
            var cosVal = Math.Cos(Radians);

            //Get points with some thread safety
            var newPointsTransformed = new List<Point>();

            //Re-transform the points
            var ptTransformed = new Point(0, 0);
            foreach (var point in Points.ToArray())
            {
                ptTransformed.X = (int)(point.X * cosVal + point.Y * sinVal);
                ptTransformed.Y = (int)(point.X * sinVal - point.Y * cosVal);

                newPointsTransformed.Add(ptTransformed);
            }

            //Add the points
            PointsTransformed.Clear();
            PointsTransformed.PushRange(newPointsTransformed.ToArray());
        }

        #endregion

        #region Movement

        /// <summary>
        /// Get the current absolute origin (top-left) of the object.
        /// </summary>
        public Point GetCurrLoc() => CurrLoc;

        /// <summary>
        /// Current absolute origin (top-left).
        /// </summary>
        protected Point CurrLoc;

        /// <summary>
        /// Get the current velocity along the X axis.
        /// </summary>
        public double GetVelocityX() => VelocityX;

        /// <summary>
        /// Current velocity along the X axis.
        /// </summary>
        protected double VelocityX;

        /// <summary>
        /// Get the current velocity along the Y axis.
        /// </summary>
        public double GetVelocityY() => VelocityY;

        /// <summary>
        /// Current velocity along the Y axis.
        /// </summary>
        protected double VelocityY;

        /// <summary>
        /// Move the object a single increment based on <see cref="GetVelocityX"/>
        /// and <see cref="GetVelocityY"/> and set current location.
        /// </summary>
        /// <returns>Indication of the move being completed successfully.</returns>
        public virtual bool Move()
        {
            CurrLoc.X += (int)VelocityX;
            CurrLoc.Y += (int)VelocityY;

            if (CurrLoc.X < 0)
                CurrLoc.X = BaseScreenCanvas.CanvasWidth - 1;
            if (CurrLoc.X >= BaseScreenCanvas.CanvasWidth)
                CurrLoc.X = 0;

            if (CurrLoc.Y < 0)
                CurrLoc.Y = BaseScreenCanvas.CanvasHeight - 1;
            if (CurrLoc.Y >= BaseScreenCanvas.CanvasHeight)
                CurrLoc.Y = 0;

            return true;
        }

        #endregion
        
    }
}
