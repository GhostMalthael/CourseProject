using CourseProject.Model;
using CourseProject.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.MessageBox;

namespace CourseProject.Pages
{
    public partial class RecordPatient : Page
    {
        private Dictionary<string, string> services = new Dictionary<string, string>();
        private Dictionary<string, string> doctors = new Dictionary<string, string>();
        private ServiceRenderedDuringVisit newServiceRenderedDuringVisit;

        public RecordPatient(ServiceRenderedDuringVisit serviceRenderedDuringVisit)
        {
            InitializeComponent();
            newServiceRenderedDuringVisit = serviceRenderedDuringVisit ?? new ServiceRenderedDuringVisit();

            LoadServicesComboBox();
            ShowTextLabel();

            if (newServiceRenderedDuringVisit.IdVisited > 0)
            {
                var visitDetails = App.Context.Visits.FirstOrDefault(x => x.IdVisit == newServiceRenderedDuringVisit.IdVisited);

                if (visitDetails != null)
                {
                    var service = App.Context.PolyclinicServices.FirstOrDefault(x => x.IdPolyclinicServices == visitDetails.ServicesProvidedDuringVisit);
                    var doctor = App.Context.Doctors.FirstOrDefault(x => x.IdDoctor == visitDetails.DocId);

                    if (service != null)
                    {
                        comboBoxService.SelectedItem = service.NamePolyclinicService;
                    }

                    if (doctor != null)
                    {
                        comboBoxDoctor.SelectedItem = doctor.FullNameDoctor;
                    }

                    textBoxNumberMedicalCard.Text = visitDetails.NumberMedicalCard.ToString();
                    dateTimePicker.Value = visitDetails.DateTimeVisit;
                }
            }
        }

        public void LoadServicesComboBox()
        {
            comboBoxService.Items.Clear();
            List<PolyclinicService> servicesList = App.Context.PolyclinicServices?.ToList();
            if (servicesList == null)
            {
                MessageBox.Show("Ошибка загрузки услуг.");
                return;
            }

            foreach (var service in servicesList)
            {
                comboBoxService.Items.Add(service.NamePolyclinicService);
                services[service.NamePolyclinicService] = service.IdPolyclinicServices.ToString();
            }
        }

        public void ShowTextLabel()
        {
            textBlockRecEdit.Text = MainWindowRegistrator.checkRecEdit ? "Запись на прием" : "Редактирование записи";
        }

        public void LoadDoctorsComboBox()
        {
            comboBoxDoctor.Items.Clear();
            if (comboBoxService.SelectedItem != null)
            {
                string selectedService = comboBoxService.SelectedItem.ToString();
                if (!services.TryGetValue(selectedService, out string serviceId))
                {
                    MessageBox.Show("Выбранная услуга не найдена.");
                    return;
                }

                var specializationsForService = App.Context?.SpecializationsServices
                    .Where(x => x.IdServiceNavigation.IdPolyclinicServices.ToString() == serviceId)
                    .Select(x => x.IdSpecializationNavigation.IdSpecialization)
                    .ToList();

                if (specializationsForService == null)
                {
                    MessageBox.Show("Ошибка загрузки специализаций.");
                    return;
                }

                var doctorsForSpecializations = App.Context?.Doctors
                    .Where(d => specializationsForService.Contains((int)d.DoctorSpecialization))
                    .ToList();

                if (doctorsForSpecializations == null)
                {
                    MessageBox.Show("Ошибка загрузки врачей.");
                    return;
                }

                foreach (var doctor in doctorsForSpecializations)
                {
                    comboBoxDoctor.Items.Add(doctor.FullNameDoctor);
                    doctors[doctor.FullNameDoctor] = doctor.IdDoctor.ToString();
                }
            }
        }

        private void comboBoxService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadDoctorsComboBox();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxDoctor.SelectedItem == null)
            {
                MessageBox.Show("Выберите врача.");
                return;
            }

            if (!dateTimePicker.Value.HasValue)
            {
                MessageBox.Show("Выберите дату и время.");
                return;
            }

            string selectedDoctorName = comboBoxDoctor.SelectedItem.ToString();
            if (!doctors.TryGetValue(selectedDoctorName, out string doctorIdStr) || !int.TryParse(doctorIdStr, out int doctorId))
            {
                MessageBox.Show("Выбранный врач не найден.");
                return;
            }

            if (comboBoxService.SelectedItem == null || !services.TryGetValue(comboBoxService.SelectedItem.ToString(), out string serviceIdStr) || !int.TryParse(serviceIdStr, out int serviceId))
            {
                MessageBox.Show("Выбранная услуга не найдена.");
                return;
            }

            DateTime selectedDateTime = dateTimePicker.Value.Value;
            DateTime intervalStart = new DateTime(selectedDateTime.Year, selectedDateTime.Month, selectedDateTime.Day, selectedDateTime.Hour, selectedDateTime.Minute / 15 * 15, 0);
            DateTime intervalEnd = intervalStart.AddMinutes(15);

            bool visitExists = App.Context.Visits.Any(v =>
                v.IdVisit != newServiceRenderedDuringVisit.IdVisited &&
                v.DocId == doctorId &&
                v.DateTimeVisit >= intervalStart &&
                v.DateTimeVisit < intervalEnd);

            if (visitExists)
            {
                MessageBox.Show("Врач уже занят на выбранное время. Выберите другое время.");
                return;
            }

            int servicePrice = App.Context.PolyclinicServices
                .Where(s => s.IdPolyclinicServices == serviceId)
                .Select(s => s.PriceService ?? 0)
                .FirstOrDefault();

            if (newServiceRenderedDuringVisit.IdVisited > 0)
            {
                var existingVisit = App.Context.Visits.FirstOrDefault(v => v.IdVisit == newServiceRenderedDuringVisit.IdVisited);
                if (existingVisit != null)
                {
                    existingVisit.DocId = doctorId;
                    existingVisit.DateTimeVisit = selectedDateTime;
                    existingVisit.NumberMedicalCard = int.Parse(textBoxNumberMedicalCard.Text);
                    existingVisit.ServicesProvidedDuringVisit = serviceId;
                    existingVisit.TotalCostVisit = servicePrice; 
                }
            }
            else
            {
                Visit newVisit = new Visit
                {
                    DocId = doctorId,
                    DateTimeVisit = selectedDateTime,
                    NumberMedicalCard = int.Parse(textBoxNumberMedicalCard.Text),
                    ServicesProvidedDuringVisit = serviceId,
                    TotalCostVisit = servicePrice 
                };

                App.Context.Visits.Add(newVisit);
                App.Context.SaveChanges();

                newServiceRenderedDuringVisit.IdVisited = newVisit.IdVisit;
                newServiceRenderedDuringVisit.IdService = serviceId;
                App.Context.ServiceRenderedDuringVisits.Add(newServiceRenderedDuringVisit);
            }

            App.Context.SaveChanges();
            MessageBox.Show("Сохранено");
            MainWindowRegistrator mainWindowRegistrator = Window.GetWindow(this) as MainWindowRegistrator;
            mainWindowRegistrator?.mainFrameRegistrator.Navigate(new MainRegistratorPage());
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindowRegistrator mainWindowRegistrator = Window.GetWindow(this) as MainWindowRegistrator;
            mainWindowRegistrator?.mainFrameRegistrator.Navigate(new MainRegistratorPage());
        }
    }
}
