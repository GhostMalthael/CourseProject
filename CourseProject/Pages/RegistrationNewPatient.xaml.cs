using CourseProject.Model;
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
using Xceed.Wpf.Toolkit;

namespace CourseProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationNewPatient.xaml
    /// </summary>
    public partial class RegistrationNewPatient : Page
    {
        public RegistrationNewPatient()
        {
            InitializeComponent();
        }

        private void buttonBackMainRegistratorPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindowRegistrator mainWindowRegistrator = Window.GetWindow(this) as MainWindowRegistrator;
            mainWindowRegistrator?.mainFrameRegistrator.Navigate(new MainRegistratorPage());
        }

        private void buttonRegistrationPatient_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = new Patient();
            var numberCard = App.Context.Patients.Any(x => x.NumberMedicalCardPatient.ToString() == textBoxNumberMedicalCardPatient.Text);
            if(string.IsNullOrEmpty(textBoxNumberMedicalCardPatient.Text) ||
                string.IsNullOrEmpty(textBoxAddressPatient.Text) ||
                string.IsNullOrEmpty(textBoxNumberPhonePatient.Text)||
                string.IsNullOrEmpty(textBoxPassportData.Text)||
                string.IsNullOrEmpty(textBoxMedicalPolicy.Text))
            {
                System.Windows.MessageBox.Show("Заполните все поля");
                return;
            }
            else
            {
                if (numberCard == true)
                {
                    System.Windows.MessageBox.Show("Такой номер медицинской карты уже существует");
                    return;
                }
                else
                {
                    if (int.TryParse(textBoxNumberMedicalCardPatient.Text, out int numberMedicalCard))
                    {
                        patient.NumberMedicalCardPatient = numberMedicalCard;
                    }
                    patient.FullNamePatient = textBoxFioPatient.Text;
                    patient.HomeAddressPatient = textBoxAddressPatient.Text;
                    patient.PhoneNumberPatient = textBoxNumberPhonePatient.Text;
                    DateTime dateBirth = birthDatePatient.SelectedDate.Value;
                    patient.BirthDatePatient = new DateOnly(dateBirth.Year, dateBirth.Month, dateBirth.Day);
                    patient.PassportDataPatient = textBoxPassportData.Text;
                    patient.MedicalPolicyPatient = textBoxMedicalPolicy.Text;
                    App.Context.Patients.Add(patient);
                    App.Context.SaveChanges();
                    System.Windows.MessageBox.Show("Клиент зарегистрирован");
                    var window = (MainWindowRegistrator)Window.GetWindow(this);
                    window.mainFrameRegistrator.Navigate(new MainRegistratorPage());
                }
            }
            
        }
    }
}

