using System;
using System.Collections.Generic;
using System.IO;
using Asteroids.Engine.Sounds;
using Asteroids.Standard.Enums;

#nullable enable

namespace Asteroids.Standard.Sounds
{
    /// <summary>
    /// Collection of <see cref="ActionSound"/> <see cref="Stream"/>s.
    /// </summary>
    internal static class ActionSounds
    {
        #region Properties

        /// <summary>
        /// Collection of <see cref="ActionSound"/> WAV file <see cref="Stream"/>s.
        /// </summary>
        private static BaseActionSounds<ActionSound> BaseActionSounds { get; }

        /// <summary>
        /// Collection of <see cref="ActionSound"/> WAV file <see cref="Stream"/>s.
        /// </summary>
        public static IReadOnlyDictionary<ActionSound, Stream> SoundDictionary => BaseActionSounds.SoundDictionary;

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
            BaseActionSounds = new BaseActionSounds<ActionSound>(new Dictionary<ActionSound, string>
            {
                {ActionSound.Fire, "fire.wav"},
                {ActionSound.Life, "life.wav"},
                {ActionSound.Thrust, "thrust.wav"},
                {ActionSound.Explode1, "explode1.wav"},
                {ActionSound.Explode2, "explode2.wav"},
                {ActionSound.Explode3, "explode3.wav"},
                {ActionSound.Saucer, "lsaucer.wav"},
            }, $"{nameof(Asteroids)}.{nameof(Standard)}", "Sounds");
        }

        #endregion
    }
}
