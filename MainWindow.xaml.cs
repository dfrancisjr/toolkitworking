using EPSTeamApp.Controls;
using EPSTeamApp.Themes;
using System.Windows;
using System.Windows.Controls;

namespace EPSTeamApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            content.Content = new MainControl();
        }

        private void notepad_Click(object sender, RoutedEventArgs e)
        {
            content.Content = new NotePad();
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            content.Content = new MainControl();
        }

        private void JIRA_Click(object sender, RoutedEventArgs e)
        {
            content.Content = new JiraControl();
        }

        private void epsabout(object sender, RoutedEventArgs e)
        {
            content.Content = new About();
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            switch (int.Parse(((MenuItem)sender).Uid))
            {
                case 0:
                    ThemesController.SetTheme(ThemesController.ThemeTypes.Light);
                    break;
                case 1:
                    ThemesController.SetTheme(ThemesController.ThemeTypes.LightWBlue);
                    break;
                case 2:
                    ThemesController.SetTheme(ThemesController.ThemeTypes.Dark);
                    break;
                case 3:
                    ThemesController.SetTheme(ThemesController.ThemeTypes.DarkWBlue);
                    break;
            }

            e.Handled = true;
        }
    }
}
