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
using System.Windows.Shapes;

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void buttonEnterInAccount_Click(object sender, RoutedEventArgs e)
        {
            var user = App.Context.Accounts.FirstOrDefault(x => x.LoginAccount == textBoxLogin.Text);
            if (string.IsNullOrEmpty(textBoxLogin.Text) || string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }
            else
            {
                if (Enter(textBoxLogin.Text, textBoxPassword.Text))
                {

                    if (user.RoleUserNavigation.NameRole == "Администратор")
                    {
                        Hide();
                        new MainWindowAdmin().Show();
                    }
                    if (user.RoleUserNavigation.NameRole == "Регистратор")
                    {
                        Hide();
                        new MainWindowRegistrator().Show();
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
        }
        public bool Enter(string login, string password)
        {
            var user = App.Context.Accounts.FirstOrDefault(x => x.LoginAccount == login);
            if (user != null)
            {
                return user.PasswordAccount == password;
            }
            return false;
        }
    }
}
