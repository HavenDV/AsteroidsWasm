using System.Windows;

namespace Asteroids.Wpf
{
    public partial class App
    {
        #region Properties

        private MainWindow? Window { get; set; }

        #endregion

        #region Overrides

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Window = new MainWindow();
            Window.Show();

        }
        protected override void OnExit(ExitEventArgs e)
        {
            Window?.Dispose();

            base.OnExit(e);
        }

        #endregion
    }
}
