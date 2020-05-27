using System;

#nullable enable

namespace Asteroids.Standard.Utilities
{
    public class Lock : IDisposable
    {
        #region Properties

        private Action Action { get; }

        #endregion

        #region Constructors

        public Lock(Action action)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            Action();
        }

        #endregion
    }
}
