using System;
using System.Collections.Generic;
using System.IO;
using Asteroids.Engine.Interfaces;
using Asteroids.Standard.Enums;

namespace Asteroids.Standard.Interfaces
{
    /// <summary>
    /// Asteroids game engine that will calculate the lines and polygons and react to control key events.
    /// </summary>
    public interface IGameController : IBaseGameController
    {
        /// <summary>
        /// Fires when the game calculation results in a sound stored in <see cref="ActionSounds"/> to be played by UI.
        /// </summary>
        event EventHandler<ActionSound> SoundPlayed;

        /// <summary>
        /// Collection (read-only) of <see cref="ActionSounds"/> used by the game engine and associated WAV <see cref="Stream"/>s.
        /// </summary>
        IReadOnlyDictionary<ActionSound, Stream> ActionSounds { get; }
    }
}
