using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CoreLibarary.Interfaces;
using CoreLibarary.Models;

namespace Kusinskiy_Timophey
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUsersService _UsersService;
        private IMenuService _MenusService;
        private IBookingsService _BookingsService;
        private IReviewsService _ReviewsService;
        public MainWindow(IUsersService UsersService, IMenuService MenuService, IBookingsService BookingsService, IReviewsService ReviewsService)
        {
            InitializeComponent();
            _UsersService = UsersService;
            _MenusService = MenuService;
            _BookingsService = BookingsService;
            _ReviewsService = ReviewsService;
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtSearch.Text))
                return true;
            else
            {
                return (item as Dish).ProductName.ToUpper().Contains(txtSearch.Text.ToUpper());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string role;
            if (_UsersService.CurrentUser.RoleId == Roles.Administrator)
            {
                role = "Администратор";
                btnUsers.IsEnabled = true;
                btnMenu.IsEnabled = true;
                btnBookings.IsEnabled = true;
                btnreviews.IsEnabled = true;
            }
            else
            {
                role = "Пользователь";
                btnUsers.IsEnabled=false;
                btnMenu.IsEnabled = false;
                btnBookings.IsEnabled = false;
                btnreviews.IsEnabled = false;
            }
            TextBlock_login.Text = _UsersService.CurrentUser.Name;
            ListViewMenu.ItemsSource = _MenusService.Menu;
            ListViewBookings.ItemsSource = _BookingsService.Get();
            TextBlock_Role.Text = role;
            Calendar.BlackoutDates.AddDatesInPast();
            var view = CollectionViewSource.GetDefaultView(ListViewMenu.ItemsSource);
            view.Filter = UserFilter;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Users = new Users(_UsersService);
            Users.ShowDialog();
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu(_MenusService, _UsersService);
            menu.ShowDialog();
        }

        private void btnBookings_Click(object sender, RoutedEventArgs e)
        {
            Bookings bookings = new Bookings(_BookingsService, _UsersService);
            bookings.ShowDialog();
        }

        private void txtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (IsLoaded)
            {
                CollectionViewSource.GetDefaultView(ListViewMenu.ItemsSource).Refresh();
            }
        }

        private void txtSearch_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[^a-zA-Zа-яА-Я0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Window_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ListViewMenu.ItemsSource = _MenusService.Get();
            var view = CollectionViewSource.GetDefaultView(ListViewMenu.ItemsSource);
            view.Filter = UserFilter;
            var date = Calendar.SelectedDate;

            ListViewBookings.ItemsSource = _BookingsService.Get();

            if (date.HasValue)
            {
                ListViewBookings.ItemsSource = _BookingsService.GGetBookedTablesByDate(new DateTimeOffset(date.Value));
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            Mouse.Capture(null);
            var date = Calendar.SelectedDate;

            if (date.HasValue)
            {
                ListViewBookings.ItemsSource = _BookingsService.GGetBookedTablesByDate(new DateTimeOffset(date.Value));
            }
        }

        private void btnreviews_Click(object sender, RoutedEventArgs e)
        {
            var Reviews = new Reviews(_UsersService, _ReviewsService);
            Reviews.ShowDialog();
        }

        private void btnLogOut_Click_1(object sender, RoutedEventArgs e)
        {
            Hide();

            var loginForm = new LoginForm(_UsersService);
            var result_log = loginForm.ShowDialog();

            if (result_log.HasValue && result_log.Value
                && _UsersService.CurrentUser != null)
            {
                Show();
                Window_Loaded(sender, e);
            }
            else
            {
                Close();
            }
        }

        private void ListViewMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var list = ListViewMenu.SelectedItem as Dish;

            if (list != null)
            {
                var window = new info(list);
                window.ShowDialog();
            }
            else
            {
            }
        }

        private void ListViewMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
