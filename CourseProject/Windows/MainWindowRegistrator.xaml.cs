using CourseProject.Model;
using CourseProject.Pages;
using CourseProject.UserControls;
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
using System.Windows.Shapes;

namespace CourseProject.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindowRegistrator.xaml
    /// </summary>
    public partial class MainWindowRegistrator : Window
    {
        public static bool checkRecEdit;
        MainRegistratorPage mainRegistratorPage = new MainRegistratorPage();
        public MainWindowRegistrator()
        {
            InitializeComponent();
        }

        private void MainFrameRegistrator_Loaded(object sender, RoutedEventArgs e)
        {
            mainFrameRegistrator.Navigate(new MainRegistratorPage());
        }

        private void RegistrationNewPatient_Click(object sender, RoutedEventArgs e)
        {

            mainFrameRegistrator.Navigate(new RegistrationNewPatient());

        }

        private void recordPatient_Click(object sender, RoutedEventArgs e)
        {
            checkRecEdit = true;
            mainFrameRegistrator.Navigate(new RecordPatient(null));

        }

        private void editRecord_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceRenderedDuringVisit.visitIdSelect != null)
            {
                mainFrameRegistrator.Navigate(new RecordPatient(ServiceRenderedDuringVisit.visitIdSelect._visit));
            }
            else
            {
                MessageBox.Show("Выберите запись");
                return;
            }
        }

        private void createReceipt_Click(object sender, RoutedEventArgs e)
        {
            mainFrameRegistrator.Navigate(new CreateOtchetPage());

        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void textBlockInformationProgram_Click(object sender, RoutedEventArgs e)
        {
            mainFrameRegistrator.Navigate(new InfoProgramPage());

        }
    }
}
