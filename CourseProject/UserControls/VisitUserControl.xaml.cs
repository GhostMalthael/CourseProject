using CourseProject.Model;
using CourseProject.Pages;
using CourseProject.Windows;
using System;
using System.Collections.Frozen;
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
    public partial class VisitUserControl : UserControl
    {
        private ServiceRenderedDuringVisit visit;
        public ServiceRenderedDuringVisit _visit
        {
            get => visit;
            set
            {
                visit = value;
                var visitDetails = App.Context.Visits.FirstOrDefault(x => x.IdVisit == visit.IdVisited);
                if (visitDetails != null)
                {
                    var service = App.Context.PolyclinicServices.FirstOrDefault(x => x.IdPolyclinicServices == visit.IdService);
                    var patient = App.Context.Patients.FirstOrDefault(x => x.NumberMedicalCardPatient == visitDetails.NumberMedicalCard);
                    var doctor = App.Context.Doctors.FirstOrDefault(x => x.IdDoctor == visitDetails.DocId);

                    if (service != null && patient != null && doctor != null)
                    {
                        textBlockService.Text = service.NamePolyclinicService;
                        textBlockDoctor.Text = doctor.FullNameDoctor;
                        textBlockNumberMedicalCard.Text = visitDetails.NumberMedicalCard.ToString();
                        textBlockFioPatient.Text = patient.FullNamePatient;
                        textBlockDateVisit.Text = visitDetails.DateTimeVisit.ToString();
                        textBlockCostVisit.Text = service.PriceService.ToString();
                    }
                }
            }
        }

        public VisitUserControl(ServiceRenderedDuringVisit visit)
        {
            InitializeComponent();
            this._visit = visit;
        }

        private void buttonCanselRecord_Click(object sender, RoutedEventArgs e)
        {
            var serviceRendered = App.Context.ServiceRenderedDuringVisits.Find(visit.Id);
            var visitCansel = App.Context.Visits.FirstOrDefault(x => x.IdVisit == serviceRendered.IdVisited);

            App.Context.ServiceRenderedDuringVisits.Remove(serviceRendered);
            App.Context.Visits.Remove(visitCansel);
            App.Context.SaveChanges();
            var window = (MainWindowRegistrator)Window.GetWindow(this);
            window.mainFrameRegistrator.Navigate(new MainRegistratorPage());
            MessageBox.Show("Запись успешно отменена.");
        }
    }
}