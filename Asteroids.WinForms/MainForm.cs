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
        private Classes.GraphicPictureBox? PictureBox { get; set; }
        private IGameController Controller { get; }

        private IDictionary<ActionSound, SoundPlayer> SoundPlayers { get; }
        private SoundPlayer? SoundPlaying { get; set; }

        public MainForm()
        {
            InitializeComponent();

            Controller = new GameController();
            Controller.SoundPlayed += OnSoundPlayed;

            SoundPlayers = Controller
                .ActionSounds
                .ToDictionary(
                    kvp => kvp.Key
                    , kvp => new SoundPlayer(kvp.Value)
                );

            foreach (var player in SoundPlayers)
                player.Value.Load();
        }

        private void OnSoundPlayed(object? sender, ActionSound sound)
        {
            if (SoundPlaying != null)
                return;

            SoundPlaying = SoundPlayers[sound];

            Task.Factory.StartNew(() =>
            {
                SoundPlaying.Stream.Position = 0;
                SoundPlaying.PlaySync();
                SoundPlaying = null;
            });
        }

        private void frmAsteroids_Closed(object sender, EventArgs e)
        {
            Controller.Dispose();
        }

        private void frmAsteroids_Resize(object? sender, EventArgs e)
        {
            var rec = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);
            Controller.ResizeGame(rec);
        }

        private async void frmAsteroids_Activated(object? sender, EventArgs e)
        {
            Activated -= frmAsteroids_Activated;
            var rec = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);
            await Controller.Initialize(PictureBox, rec);
        }

        private void frmAsteroids_KeyDown(object? sender, KeyEventArgs e)
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

        private void frmAsteroids_KeyUp(object? sender, KeyEventArgs e)
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

    }
}
