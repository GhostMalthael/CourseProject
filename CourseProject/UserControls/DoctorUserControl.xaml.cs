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

namespace CourseProject.UserControls
{
    /// <summary>
    /// Логика взаимодействия для DoctorUserControl.xaml
    /// </summary>
    public partial class DoctorUserControl : UserControl
    {
        private Doctor doctor;
        public Doctor _doctor
        {
            get => doctor;
            
            set
            {
                doctor = value;
                var specialization = App.Context.Specializations.FirstOrDefault(x => x.IdSpecialization == doctor.DoctorSpecialization);
                textBlockIdDoctor.Text = doctor.IdDoctor.ToString();
                textBlockFioDoctor.Text = doctor.FullNameDoctor;
                textBlockSpecialization.Text = specialization.NameSpecialization;
                textBlockNumberOffice.Text = doctor.NumberOffice.ToString();
            }
        }
        public DoctorUserControl(Doctor doctor)
        {
            InitializeComponent();
            _doctor = doctor;
        }
    }
}
