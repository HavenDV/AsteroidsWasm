using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Asteroids.Engine.Enums;
using Asteroids.Standard;
using Asteroids.Standard.Enums;
using Asteroids.Standard.Interfaces;
using Asteroids.Engine.Utilities;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using SharpDX.Multimedia;
using SharpDX.XAudio2;

namespace Asteroids.Avalonia
{
    public class MainWindow : Window, IDisposable
    {
        #region Properties

        private IGameController Controller { get; }
        private XAudio2 XAudio2 { get; }
        private MasteringVoice MasteringVoice { get; }
        private Locker<ActionSound> SoundLocker { get; } = new Locker<ActionSound>();

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

#if DEBUG
            this.AttachDevTools();
#endif

            XAudio2 = new XAudio2();
            MasteringVoice = new MasteringVoice(XAudio2);
            Controller = new GameController();
            Controller.SoundPlayed += OnSoundPlayed;

            Opened += OnOpened;
            KeyDown += OnHandler;
            KeyUp += OnEventHandler;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            MasteringVoice.Dispose();
            XAudio2.Dispose();
        }

        private static async Task PlaySoundAsync(XAudio2 device, Stream baseStream)
        {
            baseStream.Position = 0;

            var stream = new SoundStream(baseStream);
            await using var dataStream = stream.ToDataStream();
            var buffer = new AudioBuffer(dataStream);

            using var voice = new SourceVoice(device, stream.Format, true);
            voice.SubmitSourceBuffer(buffer, stream.DecodedPacketsInfo);
            voice.Start();

            while (voice.State.BuffersQueued > 0)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1));
            }

            voice.DestroyVoice();
        }

        #endregion

        #region Event Handlers

        private async void OnSoundPlayed(object? sender, ActionSound sound)
        {
            if (sound != ActionSound.Thrust)
            {
                await PlaySoundAsync(XAudio2, Controller.ActionSounds[sound]);
                return;
            }

            if (!SoundLocker.TryLock(sound, out var @lock))
            {
                return;
            }

            using (@lock)
            {
                await PlaySoundAsync(XAudio2, Controller.ActionSounds[sound]);
            }
        }

        private async void OnOpened(object? _, EventArgs args)
        {
            await Controller.Initialize(
                this.FindControl<Controls.GraphicContainer>("MainContainer"),
                new Rectangle(0, 0, (int)Width, (int)Height));
        }

        private void OnHandler(object? _, KeyEventArgs args)
        {
            PlayKey key;
            switch (args.Key)
            {
                case Key.Escape:
                    // Escape during a title screen exits the game
                    if (Controller.GameStatus == GameMode.Title)
                    {
                        Close();
                        return;
                    }

                    key = PlayKey.Escape;
                    break;

                case Key.Left:
                    key = PlayKey.Left;
                    break;

                case Key.Right:
                    key = PlayKey.Right;
                    break;

                case Key.Up:
                    key = PlayKey.Up;
                    break;

                case Key.Down:
                    key = PlayKey.Down;
                    break;

                case Key.Space:
                    key = PlayKey.Space;
                    break;

                case Key.P:
                    key = PlayKey.P;
                    break;

                case Key.D1:
                    key = PlayKey.One;
                    break;

                case Key.D2:
                    key = PlayKey.Two;
                    break;

                default:
                    return;
            }

            Controller.KeyDown(key);
        }

        private void OnEventHandler(object? _, KeyEventArgs args)
        {
            PlayKey key;
            switch (args.Key)
            {
                case Key.Escape:
                    key = PlayKey.Escape;
                    break;

                case Key.Left:
                    key = PlayKey.Left;
                    break;

                case Key.Right:
                    key = PlayKey.Right;
                    break;

                case Key.Up:
                    key = PlayKey.Up;
                    break;

                case Key.Down:
                    key = PlayKey.Down;
                    break;

                case Key.Space:
                    key = PlayKey.Space;
                    break;

                case Key.P:
                    key = PlayKey.P;
                    break;

                default:
                    return;
            }

            Controller.KeyUp(key);
        }

        #endregion
    }
}
