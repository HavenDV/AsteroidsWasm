using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Asteroids.Engine.Sounds
{
    /// <summary>
    /// Collection of <see cref="Stream"/>s.
    /// </summary>
    public class BaseActionSounds<T>
    {
        #region Properties

        /// <summary>
        /// Collection of <see cref="Stream"/> WAV file <see cref="Stream"/>s.
        /// </summary>
        public IReadOnlyDictionary<T, Stream> SoundDictionary { get; }

        #endregion

        #region Constructors

        public BaseActionSounds(
            IReadOnlyDictionary<T, string> dictionary, 
            string assemblyName, 
            string soundDir)
        {
            var assembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .First(a => a.GetName().Name == assemblyName);

            var dirName = $"{assemblyName}.{soundDir}";
            SoundDictionary = dictionary.ToDictionary(
                pair => pair.Key,
                pair => assembly.GetManifestResourceStream($"{dirName}.{pair.Value}"));
        }

        #endregion
    }
}
