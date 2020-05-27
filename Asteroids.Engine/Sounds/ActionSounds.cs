using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Asteroids.Standard.Enums;

#nullable enable

namespace Asteroids.Standard.Sounds
{
    /// <summary>
    /// Collection of <see cref="ActionSound"/> <see cref="Stream"/>s.
    /// </summary>
    internal static class ActionSounds
    {
        #region Constants

        private const string SoundDir = "Sounds";

        #endregion

        #region Properties

        /// <summary>
        /// Collection of <see cref="ActionSound"/> WAV file <see cref="Stream"/>s.
        /// </summary>
        public static IReadOnlyDictionary<ActionSound, Stream> SoundDictionary { get; }

        #endregion

        #region Events

        /// <summary>
        /// Fires when a call is made within the game engine to play a sound.
        /// </summary>
        public static event EventHandler<ActionSound>? SoundTriggered;

        /// <summary>
        /// Invokes <see cref="SoundTriggered"/> to play an <see cref="ActionSound"/>.
        /// </summary>
        /// <param name="sender">Calling object.</param>
        /// <param name="sound">Sound to play.</param>
        public static void OnSoundTriggered(object sender, ActionSound sound)
        {
            SoundTriggered?.Invoke(sender, sound);
        }

        #endregion

        #region Constructors

        static ActionSounds()
        {
            var assemblyName = $"{nameof(Asteroids)}.{nameof(Standard)}";
            var dirName = $"{assemblyName}.{SoundDir}";
            var assembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .First(a => a.GetName().Name == assemblyName);

            SoundDictionary = new Dictionary<ActionSound, Stream>
            {
                {ActionSound.Fire, assembly.GetManifestResourceStream($"{dirName}.fire.wav")},
                {ActionSound.Life, assembly.GetManifestResourceStream($"{dirName}.life.wav")},
                {ActionSound.Thrust, assembly.GetManifestResourceStream($"{dirName}.thrust.wav")},
                {ActionSound.Explode1, assembly.GetManifestResourceStream($"{dirName}.explode1.wav")},
                {ActionSound.Explode2, assembly.GetManifestResourceStream($"{dirName}.explode2.wav")},
                {ActionSound.Explode3, assembly.GetManifestResourceStream($"{dirName}.explode3.wav")},
                {ActionSound.Saucer, assembly.GetManifestResourceStream($"{dirName}.lsaucer.wav")},
            };
        }

        #endregion
    }
}
