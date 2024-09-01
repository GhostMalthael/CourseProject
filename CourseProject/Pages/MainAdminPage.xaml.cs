using CourseProject.Model;
using CourseProject.UserControls;
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
    /// Логика взаимодействия для MainAdminPage.xaml
    /// </summary>
    public partial class MainAdminPage : Page
    {
        List<Doctor> doctors = App.Context.Doctors.ToList();
        public MainAdminPage()
        {
            InitializeComponent();
            LoadDoctors();
        }
        public void LoadDoctors()
        {
                listViewMainWindowAdmin.ItemsSource = doctors.Select(d => new DoctorUserControl(d)).ToList();
        }

        private void listViewMainWindowAdmin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listViewMainWindowAdmin.SelectedItems != null)
            {
                var id = listViewMainWindowAdmin.SelectedItem as DoctorUserControl;
                Doctor.id = id;
            }
        }
    }
}
