using System.Collections.Concurrent;

#nullable enable

namespace Asteroids.Standard.Utilities
{
    public class Locker<T>
    {
        #region Properties

        private ConcurrentDictionary<T, bool> PlayingSounds { get; } = new ConcurrentDictionary<T, bool>();

        #endregion

        #region Methods

        public bool TryLock(T value, out Lock @lock)
        {
            @lock = new Lock(() => PlayingSounds.TryRemove(value, out _));

            return PlayingSounds.AddOrUpdate(value, true, (o, _) => false);
        }

        #endregion
    }
}
