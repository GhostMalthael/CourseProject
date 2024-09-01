using CourseProject.Model;
using System.Configuration;
using System.Data;
using System.Windows;

namespace CourseProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static PaidPolyclinicContext Context { get; } = new PaidPolyclinicContext();
    }

}
