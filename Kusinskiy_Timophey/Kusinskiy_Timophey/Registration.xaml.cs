using CoreLibarary.Interfaces;
using System.Windows;
using System;
using System.Text.RegularExpressions;

namespace Kusinskiy_Timophey
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private IUsersService _UsersService;
        public Registration(IUsersService usersService)
        {
            InitializeComponent();
            _UsersService = usersService;
            combobox_Role.SelectedIndex = 1;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtName.Text == ""
                || txtPhone.Text == ""
                || txtEmail.Text == ""
                || pswPass.Password == ""
                || pswPass_check.Password == "")
                {
                    throw new Exception($"Заполните все поля");
                }
                else
                {
                    string Name = txtName.Text;
                    string Phone = txtPhone.Text;
                    string Email = txtEmail.Text;
                    string Password = pswPass.Password;
                    int RoleId = combobox_Role.SelectedIndex;
                    switch (RoleId)
                    {
                        case 0:
                            RoleId = 1;
                            break;
                        case 1:
                            RoleId = 2;
                            break;
                    }
                    if (pswPass.Password == pswPass_check.Password)
                    {
                        var isRegedIn = _UsersService.Create(Name, Phone, Email, Password, RoleId);
                        DialogResult = true;
                    }
                    else
                    {
                        throw new Exception("Пароли не совпадают");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Can't Register. Error: '{ex.Message}'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtName.Focus();
        }

        private void txtPhone_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[^+]*[^0-9+]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtName_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[^a-zA-Zа-яА-Я0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtEmail_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[^a-zA-Zа-яА-Я0-9@.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void pswPass_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[^a-zA-Zа-яА-Я0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void pswPass_check_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[^a-zA-Zа-яА-Я0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}