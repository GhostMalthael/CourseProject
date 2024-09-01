using CourseProject.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainRegistratorPage.xaml
    /// </summary>
    public partial class MainRegistratorPage : Page
    {
        List<ServiceRenderedDuringVisit> visits = App.Context.ServiceRenderedDuringVisits.ToList();

        public MainRegistratorPage()
        {
            InitializeComponent();
            LoadVisits();
        }

        public void LoadVisits()
        {
            listViewMainRegistratorWindow.Items.Clear();
            foreach (var visit in visits)
            {
                listViewMainRegistratorWindow.Items.Add(new VisitUserControl(visit));
            }
        }

        private void listViewMainRegistratorWindow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewMainRegistratorWindow.SelectedItems != null)
            {
                var id = listViewMainRegistratorWindow.SelectedItem as VisitUserControl;
                ServiceRenderedDuringVisit.visitIdSelect = id;
            }
        }
    }
}
