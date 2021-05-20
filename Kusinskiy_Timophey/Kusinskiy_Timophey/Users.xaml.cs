using System;
using System.Text.RegularExpressions;
using System.Windows;
using CoreLibarary.Interfaces;
using CoreLibarary.Models;

namespace Kusinskiy_Timophey
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        private IUsersService _UsersService;

        public Users(IUsersService UsersService)
        {
            InitializeComponent();
            _UsersService = UsersService;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewUsers.ItemsSource = _UsersService.Get();
            btnDel.IsEnabled = false;
            btnEdite.IsEnabled = false;
            txtPhone.IsEnabled = false;
            txtEmail.IsEnabled = false;
            combobox_Role.IsEnabled = false;
        }

        private void ListViewUsers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var list = ListViewUsers.SelectedItem as User;
            var user = new User();
            if (list != null)
            {
                if (list.Id == _UsersService.CurrentUser.Id)
                {
                    btnDel.IsEnabled = false;
                    btnEdite.IsEnabled = false;
                    txtPhone.IsEnabled = false;
                    txtEmail.IsEnabled = false;
                    combobox_Role.IsEnabled = false;
                }
                else
                {
                    btnEdite.IsEnabled = true;
                    btnDel.IsEnabled = true;
                    txtPhone.IsEnabled = true;
                    txtEmail.IsEnabled = true;
                    combobox_Role.IsEnabled = true;
                }
                txtPhone.Text = list.Phone;
                txtEmail.Text = list.Email;
                combobox_Role.SelectedIndex = (int)list.RoleId - 1;
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var del = ListViewUsers.SelectedItem as User;
                var user = new User();
                try
                {
                    user.Id = del.Id;
                }
                catch
                {
                    throw new Exception("Выберите пользователя");
                }
                _UsersService.Delete(del.Id);
                ListViewUsers.ItemsSource = _UsersService.Get();
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Can't delet accaunt'{ex.Message}'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var list = ListViewUsers.SelectedItem as User;
                var user = new User();
                try
                {
                    user.Id = list.Id;
                }
                catch
                {
                    throw new Exception("Выберите пользователя");
                }
                user.Phone = txtPhone.Text;
                user.Email = txtEmail.Text;
                switch (combobox_Role.SelectedIndex)
                {
                    case 0:
                        user.RoleId = (Roles)1;
                        break;
                    case 1:
                        user.RoleId = (Roles)2;
                        break;
                }
                _UsersService.Update(user);
                ListViewUsers.ItemsSource = _UsersService.Get();
                Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Can't edite accaunt'{ex.Message}'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Clear()
        {
            btnEdite.IsEnabled = false;
            btnDel.IsEnabled = false;
            txtPhone.Clear();
            txtEmail.Clear();
            combobox_Role.SelectedIndex = -1;
        }

        private void txtPhone_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[^+]*[^0-9+]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void txtEmail_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[^a-zA-Zа-яА-Я0-9@.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}