using CoreLibarary.Interfaces;
using CoreLibarary.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Kusinskiy_Timophey
{
    /// <summary>
    /// Логика взаимодействия для Reviews.xaml
    /// </summary>
    public partial class Reviews : Window
    {

        private IUsersService _UsersService;
        private IReviewsService _ReviewsService;

        public Reviews(IUsersService UsersService, IReviewsService ReviewsService)
        {
            InitializeComponent();
            _UsersService = UsersService;
            _ReviewsService = ReviewsService;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtName.Text == "" ||
                    txtDescriptions.Text ==  "" )
                {
                    throw new Exception("Заполните поля");
                }
                else
                {
                    var list = ListViewUsers.SelectedItem as User;
                    var id = list.Id;
                    txtName.Text = list.Name;
                    _ReviewsService.Create(_UsersService.CurrentUser.Id, txtDescriptions.Text, id);
                    ListViewReviews.ItemsSource = _ReviewsService.Get();
                    ListViewReviews.Items.Refresh();
                    ListViewUsers.ItemsSource = _UsersService.Get();
                    ListViewUsers.SelectedIndex = -1;
                    txtName.Clear();
                    txtDescriptions.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Can't add review:'{ex.Message}'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewReviews.ItemsSource = _ReviewsService.Get();
            ListViewUsers.ItemsSource = _UsersService.Get();
        }

        private void ListViewUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = ListViewUsers.SelectedItem as User;
            if (list != null)
            {
                txtName.Text = list.Name;
                if (list.Id == _UsersService.CurrentUser.Id)
                {
                    txtDescriptions.IsEnabled = false;
                    txtDescriptions.Text = "Вы не можете написать отзыв о самом себе";
                    btnAdd.IsEnabled = false;
                }
                else
                {
                    txtDescriptions.IsEnabled = true;
                    btnAdd.IsEnabled = true;
                    txtDescriptions.Clear();
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}