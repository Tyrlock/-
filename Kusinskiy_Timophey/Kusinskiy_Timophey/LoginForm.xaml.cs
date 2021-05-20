using CoreLibarary.Interfaces;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace Kusinskiy_Timophey
{
    /// <summary>
    /// Логика взаимодействия для Entrance.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        private IUsersService _UsersService;

        public LoginForm(IUsersService usersService)
        {
            InitializeComponent();
            _UsersService = usersService;
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var isLogedIn = _UsersService.Login(txtName.Text, pswPass.Password);

                if (isLogedIn)
                {
                    DialogResult = true;
                }
                else
                {
                    throw new Exception("Данного пользователя не существует");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Can't login. Error: '{ex.Message}'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var Registration = new Registration(_UsersService);
            Registration.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtName.Focus();
        }
        private System.Collections.Generic.List<string> itemsList = null;
        public System.Collections.Generic.List<string> ItemsList
        {
            get
            {
                if (itemsList == null)
                {
                    itemsList = new System.Collections.Generic.List<string>();
                    itemsList.Add("1");
                    itemsList.Add("2");
                }
                return itemsList;
            }
        }

        private void txtName_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[^a-zA-Zа-яА-Я0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void pswPass_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[^a-zA-Zа-яА-Я0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Привет");
        }
    }
}
