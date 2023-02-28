using System;
using System.Windows;
using System.Windows.Threading;

namespace EPSTeamApp
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        DispatcherTimer dT = new DispatcherTimer();

        public Splash()
        {
            InitializeComponent();
            dT.Tick += new EventHandler(dt_Tick);
            dT.Interval = new TimeSpan(0, 0, 7);
            dT.Start();
        }
        private void dt_Tick(object sender, EventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();

            dT.Stop();
            this.Close();
        }
    }
}
