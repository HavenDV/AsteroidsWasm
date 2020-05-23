using System.ComponentModel;

namespace Asteroids.WinForms
{
    partial class MainForm
    {
        #region Setup

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly Container _components = null;

        protected override void Dispose(bool disposing)
        {
            Controller.SoundPlayed -= OnSoundPlayed;

            if (disposing)
                _components?.Dispose();


            foreach (var player in SoundPlayers)
                player.Value.Dispose();

            Controller.Dispose();

            base.Dispose(disposing);
        }

        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PictureBox = new Classes.GraphicPictureBox();
            ((ISupportInitialize)(this.PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _frame1
            // 
            this.PictureBox.BackColor = System.Drawing.SystemColors.WindowText;
            this.PictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBox.Location = new System.Drawing.Point(0, 0);
            this.PictureBox.Name = "_frame1";
            this.PictureBox.Size = new System.Drawing.Size(634, 461);
            this.PictureBox.TabIndex = 2;
            this.PictureBox.TabStop = false;
            // 
            // FrmAsteroids
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(12, 28);
            this.ClientSize = new System.Drawing.Size(634, 461);
            this.Controls.Add(this.PictureBox);
            this.Name = "FrmAsteroids";
            this.Text = "Asteroids";
            this.Activated += new System.EventHandler(this.frmAsteroids_Activated);
            this.Closed += new System.EventHandler(this.frmAsteroids_Closed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAsteroids_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmAsteroids_KeyUp);
            this.Resize += new System.EventHandler(this.frmAsteroids_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

    }
}