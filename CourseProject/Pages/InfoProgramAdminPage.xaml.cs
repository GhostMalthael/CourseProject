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
    /// Логика взаимодействия для InfoProgramAdminPage.xaml
    /// </summary>
    public partial class InfoProgramAdminPage : Page
    {
        public InfoProgramAdminPage()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindowAdmin mainWindowAdmin = Window.GetWindow(this) as MainWindowAdmin;
            mainWindowAdmin?.MainFrameAdmin.Navigate(new MainAdminPage());
        }
    }
}
