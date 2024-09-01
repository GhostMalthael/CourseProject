using CourseProject.Model;
using CourseProject.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CourseProject.Pages
{
    public partial class CreateOtchetPage : Page
    {
        private Dictionary<string, string> services = new Dictionary<string, string>();

        public CreateOtchetPage()
        {
            InitializeComponent();
            LoadComboBox();
        }

        public void LoadComboBox()
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

        private void buttonCreateOtchet_Click(object sender, RoutedEventArgs e)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            string folderPath = @"C:\Users\alexc\source\repos\CourseProject\CourseProject\Doc\";

            var pdfDoc = new iTextSharp.text.Document();

            DateTime? selectedDate = datePickerOtchetService.SelectedDate;

            if (!selectedDate.HasValue)
            {
                MessageBox.Show("Выберите дату.");
                return;
            }

            string formattedDate = selectedDate.Value.ToString("yyyyMMdd");

            string reportDate = selectedDate.Value.ToString("dd.MM.yyyy");

            string reportFileName = $"Отчет_{formattedDate}.pdf";

            PdfWriter.GetInstance(pdfDoc, new FileStream(Path.Combine(folderPath, reportFileName), FileMode.Create));

            pdfDoc.Open();

            BaseFont bf = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font font = new Font(bf, 12);

            Paragraph header = new Paragraph($"Отчет за {reportDate}", font)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 20 
            };
            pdfDoc.Add(header);

            var pdfTable = new PdfPTable(datagridServicesOtchet.Columns.Count);

            foreach (DataGridColumn column in datagridServicesOtchet.Columns)
            {
                pdfTable.AddCell(new Phrase(column.Header.ToString(), font));
            }

            foreach (var item in datagridServicesOtchet.Items)
            {
                if (item is OtchetItem otchetItem)
                {
                    pdfTable.AddCell(new Phrase(otchetItem.NameService, font));
                    pdfTable.AddCell(new Phrase(otchetItem.FullNameDoctor, font));
                    pdfTable.AddCell(new Phrase(otchetItem.NumberMedicalCardPatient, font));
                    pdfTable.AddCell(new Phrase(otchetItem.PriceService.ToString(), font));
                }
            }

            pdfDoc.Add(pdfTable);

            pdfDoc.Close();

            MessageBox.Show("Отчет успешно создан и сохранен в " + folderPath);
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindowRegistrator)Window.GetWindow(this);
            mainWindow.mainFrameRegistrator.Navigate(new MainRegistratorPage());
        }
        private void datePickerOtchetService_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxService.SelectedItem == null)
            {
                MessageBox.Show("Выберите услугу.");
                return;
            }

            if (!services.TryGetValue(comboBoxService.SelectedItem.ToString(), out string serviceIdStr))
            {
                MessageBox.Show("Выбранная услуга не найдена.");
                return;
            }

            if (!int.TryParse(serviceIdStr, out int serviceId))
            {
                MessageBox.Show("Ошибка при преобразовании Id услуги в число.");
                return;
            }

            DateTime? selectedDate = datePickerOtchetService.SelectedDate;

            if (!selectedDate.HasValue)
            {
                MessageBox.Show("Выберите дату.");
                return;
            }

            DateTime truncatedDate = selectedDate.Value.Date;

            var otchetItems = App.Context.Visits
                .Where(v => v.ServicesProvidedDuringVisit == serviceId && v.DateTimeVisit.HasValue && v.DateTimeVisit.Value.Date == truncatedDate)
                .ToList()
                .Select(v => new OtchetItem
                {
                    NameService = App.Context.PolyclinicServices.FirstOrDefault(s => s.IdPolyclinicServices == v.ServicesProvidedDuringVisit)?.NamePolyclinicService ?? string.Empty,
                    FullNameDoctor = App.Context.Doctors.FirstOrDefault(d => d.IdDoctor == v.DocId)?.FullNameDoctor ?? string.Empty,
                    NumberMedicalCardPatient = v.NumberMedicalCard.ToString(),
                    PriceService = App.Context.PolyclinicServices.FirstOrDefault(s => s.IdPolyclinicServices == v.ServicesProvidedDuringVisit)?.PriceService ?? 0
                })
                .ToList();

            if (otchetItems.Count == 0)
            {
                MessageBox.Show("Нет данных для выбранной услуги и даты.");
            }

            datagridServicesOtchet.ItemsSource = otchetItems;
        }
    }


    public class OtchetItem
    {
        public string NameService { get; set; } 
        public string FullNameDoctor { get; set; }
        public string NumberMedicalCardPatient { get; set; }
        public int PriceService { get; set; }
    }

}
