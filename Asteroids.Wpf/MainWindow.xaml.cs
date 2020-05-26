using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Asteroids.Standard;
using Asteroids.Standard.Enums;
using Asteroids.Standard.Interfaces;

namespace Asteroids.Wpf
{
    public partial class MainWindow : IDisposable
    {
        #region Properties

        private IGameController Controller { get; }
        private IDictionary<ActionSound, SoundPlayer> SoundPlayers { get; }
        private SoundPlayer? ActiveSoundPlayer { get; set; }

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

            Controller = new GameController();

            SoundPlayers = Controller
                .ActionSounds
                .ToDictionary(
                    pair => pair.Key, 
                    pair => new SoundPlayer(pair.Value));

            foreach (var player in SoundPlayers)
            {
                player.Value.Load();
            }

            Controller.SoundPlayed += OnSoundPlayed;
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            Controller.SoundPlayed -= OnSoundPlayed;

            foreach (var player in SoundPlayers)
            {
                player.Value.Dispose();
            }

            Controller.Dispose();
            MainContainer.Dispose();
        }

        #endregion

        #region Event Handlers

        private void OnSoundPlayed(object? sender, ActionSound sound)
        {
            if (ActiveSoundPlayer != null)
            {
                return;
            }

            ActiveSoundPlayer = SoundPlayers[sound];

            Task.Factory.StartNew(() =>
            {
                ActiveSoundPlayer.Stream.Position = 0;
                ActiveSoundPlayer.PlaySync();
                ActiveSoundPlayer = null;
            });
        }

        private async void MainWindow_OnLoaded(object? sender, RoutedEventArgs e)
        {
            await Controller.Initialize(
                MainContainer,
                new Rectangle(0, 0, (int)MainContainer.ActualWidth, (int)MainContainer.ActualHeight));
        }

        private void Window_SizeChanged(object? sender, SizeChangedEventArgs e)
        {
            Controller.ResizeGame(
                new Rectangle(0, 0, (int)e.NewSize.Width, (int)e.NewSize.Height));
        }

        private void Window_KeyDown(object? sender, KeyEventArgs e)
        {
            PlayKey key;
            switch (e.Key)
            {
                case Key.Escape:
                    // Escape during a title screen exits the game
                    if (Controller.GameStatus == GameMode.Title)
                    {
                        Application.Current.Shutdown();
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

                default:
                    return;
            }

            Controller.KeyDown(key);
        }

        private void Window_KeyUp(object? sender, KeyEventArgs e)
        {
            PlayKey key;
            switch (e.Key)
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
