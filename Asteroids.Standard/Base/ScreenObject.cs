using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Asteroids.Engine.Base;
using Asteroids.Standard.Components;
using Asteroids.Standard.Screen;

namespace Asteroids.Standard.Base
{
    /// <summary>
    /// ScreenObject - defines an object to be displayed on screen
    /// This object is based on a cartesian coordinate system 
    /// centered at 0, 0
    /// </summary>
    internal abstract class ScreenObject : BaseScreenObject
    {
        #region Properties

        /// <summary>
        /// Relative time length at which the object explodes.
        /// </summary>
        protected int ExplosionLength { get; set; } = ScreenCanvas.DefaultExplosionLength;

        /// <summary>
        /// Indicates if the object is alive.
        /// </summary>
        public bool IsAlive { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="ScreenObject"/>.
        /// </summary>
        /// <param name="location">Absolute origin (bottom-left) of the object.</param>
        protected ScreenObject(Point location) : base(location)
        {
            IsAlive = true;
        }

        #endregion

        #region State

        /// <summary>
        /// Blow up the object.
        /// </summary>
        /// <returns>Explosion collection.</returns>
        public virtual IList<Explosion> Explode()
        {
            IsAlive = false;

            VelocityX = 0;
            VelocityY = 0;

            return GetPoints()
                .Select(p => new Explosion(p, ExplosionLength))
                .ToList();
        }

        #endregion
    }
}
