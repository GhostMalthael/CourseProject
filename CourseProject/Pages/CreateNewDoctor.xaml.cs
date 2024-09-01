using CourseProject.Model;
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
using Microsoft.EntityFrameworkCore;
using CourseProject.Windows;


namespace CourseProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreateNewDoctor.xaml
    /// </summary>
    public partial class CreateNewDoctor : Page
    {
        Dictionary<string, string> specializations = new Dictionary<string, string>();
        MainAdminPage adminPage = new MainAdminPage();
        Doctor doctor = new Doctor();

        public CreateNewDoctor(Doctor doctor)
        {
            InitializeComponent();
            LoadSpecializations();
            if (doctor != null)
            {
                this.doctor = doctor;
                this.DataContext = doctor;
            }
        }

        private void LoadSpecializations()
        {
            comboBoxSpecialization.Items.Clear();
            List<Specialization> specializations = App.Context.Specializations.ToList();    
            foreach (var specialization in specializations) 
            {
                comboBoxSpecialization.Items.Add(specialization.NameSpecialization);
                this.specializations[specialization.NameSpecialization] = specialization.IdSpecialization.ToString();
            }
        }

        private void buttonRegistrationDoctor_Click(object sender, RoutedEventArgs e)
        {
            if(doctor.IdDoctor < 0)
            {
                string fullName = textBoxFioDoctor.Text.Trim();
                string officeNumberStr = textBoxNumberOffice.Text.Trim();

                if (!int.TryParse(officeNumberStr, out int officeNumber))
                {
                    MessageBox.Show("Номер кабинета должен быть числом.");
                    return;
                }

                if (string.IsNullOrEmpty(fullName) || comboBoxSpecialization.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.");
                    return;
                }

                string selectedSpecializationName = comboBoxSpecialization.SelectedItem.ToString();

                var selectedSpecialization = App.Context.Specializations
                    .FirstOrDefault(s => s.NameSpecialization == selectedSpecializationName);

                if (selectedSpecialization == null)
                {
                    MessageBox.Show("Выбранная специализация не найдена в базе данных.");
                    return;
                }

                int maxDoctorId = App.Context.Doctors.Max(d => (int?)d.IdDoctor) ?? 0;


                doctor.IdDoctor = maxDoctorId + 1;
                doctor.FullNameDoctor = fullName;
                doctor.NumberOffice = officeNumber;
                doctor.DoctorSpecializationNavigation = selectedSpecialization;
                
                App.Context.Doctors.Add(doctor);
            }

            App.Context.SaveChanges();

            MessageBox.Show("Сохранено");

            var window = (MainWindowAdmin)Window.GetWindow(this);
            window.MainFrameAdmin.Navigate(new MainAdminPage());
        }

        private void buttonBackMainAdminPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindowAdmin mainWindowAdmin = Window.GetWindow(this) as MainWindowAdmin;
            mainWindowAdmin?.MainFrameAdmin.Navigate(new MainAdminPage());
        }
    }
}

