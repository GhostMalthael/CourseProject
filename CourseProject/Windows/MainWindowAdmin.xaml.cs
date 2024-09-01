using CourseProject.Model;
using CourseProject.Pages;
using CourseProject.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseProject.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindowAdmin.xaml
    /// </summary>
    public partial class MainWindowAdmin : Window
    {
        MainAdminPage adminPage = new MainAdminPage();
        public MainWindowAdmin()
        {
            InitializeComponent();
        }

        private void MainFrameAdmin_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrameAdmin.Navigate(new MainAdminPage());
        }

        private void textBlockCreateNewDoctor_Click(object sender, RoutedEventArgs e)
        {
            MainFrameAdmin.Navigate(new CreateNewDoctor(null));
        }

        private void textBlockEditDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (Doctor.id != null)
            {
               MainFrameAdmin.Navigate(new CreateNewDoctor(Doctor.id._doctor));
            }
            else
            {
                MessageBox.Show("Выберите врача");
                return;
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void textBlockInformationProgram_Click(object sender, RoutedEventArgs e)
        {
            MainFrameAdmin.Navigate(new InfoProgramAdminPage());

        }
    }
}
