using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Asteroids.Engine.Sounds
{
    /// <summary>
    /// Collection of <see cref="Stream"/>s.
    /// </summary>
    public class BaseActionSounds
    {
        #region Properties

        /// <summary>
        /// Collection of <see cref="Stream"/> WAV file <see cref="Stream"/>s.
        /// </summary>
        public IReadOnlyDictionary<string, Stream> SoundDictionary { get; }

        #endregion

        #region Events

        /// <summary>
        /// Fires when a call is made within the game engine to play a sound.
        /// </summary>
        public event EventHandler<Stream>? SoundTriggered;

        /// <summary>
        /// Invokes <see cref="SoundTriggered"/> to play an <see cref="Stream"/>.
        /// </summary>
        /// <param name="sender">Calling object.</param>
        /// <param name="value">Sound to play.</param>
        public void OnSoundTriggered(object sender, Stream value)
        {
            SoundTriggered?.Invoke(sender, value);
        }

        #endregion

        #region Constructors

        public BaseActionSounds(
            IEnumerable<string> names, 
            string assemblyName, 
            string soundDir)
        {
            var assembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .First(a => a.GetName().Name == assemblyName);

            var dirName = $"{assemblyName}.{soundDir}";
            SoundDictionary = names.ToDictionary(
                name => name,
                name => assembly.GetManifestResourceStream($"{dirName}.{name}"));
        }

        #endregion
    }
}
