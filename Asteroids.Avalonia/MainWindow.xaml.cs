using System;
using System.Drawing;
using Asteroids.Standard;
using Asteroids.Standard.Enums;
using Asteroids.Standard.Interfaces;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

#nullable enable

namespace Asteroids.Avalonia
{
    public class MainWindow : Window
    {
        #region Properties

        private IGameController Controller { get; }

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
            
#if DEBUG
            this.AttachDevTools();
#endif

            Controller = new GameController();
            Opened += OnOpened;
            KeyDown += OnHandler;
            KeyUp += OnEventHandler;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        #endregion

        #region Event Handlers

        private async void OnOpened(object? _, EventArgs args)
        {
            await Controller.Initialize(this.FindControl<Controls.GraphicContainer>("MainContainer"), new Rectangle(0, 0, (int)Width, (int)Height));
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
