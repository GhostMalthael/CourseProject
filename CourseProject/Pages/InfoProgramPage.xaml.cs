using CourseProject.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для InfoProgramPage.xaml
    /// </summary>
    public partial class InfoProgramPage : Page
    {
        public InfoProgramPage()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindowRegistrator mainWindowRegistrator = Window.GetWindow(this) as MainWindowRegistrator;
            mainWindowRegistrator?.mainFrameRegistrator.Navigate(new MainRegistratorPage());
        }
    }
}
