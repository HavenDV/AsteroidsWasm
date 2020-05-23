using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using Asteroids.Standard;
using Asteroids.Standard.Enums;
using Asteroids.Standard.Interfaces;

namespace Asteroids.WinForms
{
    public partial class MainForm : Form
    {
        #region Properties

        private IGameController Controller { get; }
        private IDictionary<ActionSound, SoundPlayer> SoundPlayers { get; }
        private SoundPlayer? ActiveSoundPlayer { get; set; }

        #endregion

        #region Constructors

        public MainForm()
        {
            InitializeComponent();

            Controller = new GameController();
            Controller.SoundPlayed += OnSoundPlayed;

            SoundPlayers = Controller
                .ActionSounds
                .ToDictionary(
                    pair => pair.Key, 
                    pair => new SoundPlayer(pair.Value)
                );

            foreach (var player in SoundPlayers)
            {
                player.Value.Load();
            }
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

        private void MainForm_Resize(object? sender, EventArgs e)
        {
            Controller.ResizeGame(new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));
        }

        private async void MainForm_Load(object? sender, EventArgs e)
        {
            await Controller.Initialize(PictureBox, new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));
        }

        private void MainForm_KeyDown(object? sender, KeyEventArgs e)
        {
            PlayKey key;
            switch (e.KeyData)
            {
                case Keys.Escape:
                    // Escape during a title screen exits the game
                    if (Controller.GameStatus == GameMode.Title)
                    {
                        Application.Exit();
                        return;
                    }

                    key = PlayKey.Escape;
                    break;

                case Keys.Left:
                    key = PlayKey.Left;
                    break;

                case Keys.Right:
                    key = PlayKey.Right;
                    break;

                case Keys.Up:
                    key = PlayKey.Up;
                    break;

                case Keys.Down:
                    key = PlayKey.Down;
                    break;

                case Keys.Space:
                    key = PlayKey.Space;
                    break;

                case Keys.P:
                    key = PlayKey.P;
                    break;

                default:
                    return;
            }

            Controller.KeyDown(key);
        }

        private void MainForm_KeyUp(object? sender, KeyEventArgs e)
        {
            PlayKey key;
            switch (e.KeyData)
            {
                case Keys.Escape:
                    key = PlayKey.Escape;
                    break;

                case Keys.Left:
                    key = PlayKey.Left;
                    break;

                case Keys.Right:
                    key = PlayKey.Right;
                    break;

                case Keys.Up:
                    key = PlayKey.Up;
                    break;

                case Keys.Down:
                    key = PlayKey.Down;
                    break;

                case Keys.Space:
                    key = PlayKey.Space;
                    break;

                case Keys.P:
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
