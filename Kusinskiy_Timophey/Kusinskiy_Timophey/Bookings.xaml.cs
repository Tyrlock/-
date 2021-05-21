using CoreLibarary.Interfaces;
using CoreLibarary.Models;
using System;
using System.Text.RegularExpressions;
using System.Windows;


namespace Kusinskiy_Timophey
{
    /// <summary>
    /// Логика взаимодействия для Bookings.xaml
    /// </summary>
    public partial class Bookings : Window
    {
        private IBookingsService _BookingsService;
        private IUsersService _UsersService;

        public Bookings(IBookingsService BookinsService, IUsersService UsersService)
        {
            InitializeComponent();
            _BookingsService = BookinsService;
            _UsersService = UsersService;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewBookings.ItemsSource = _BookingsService.Bookings;
            ListViewTables.ItemsSource = _BookingsService.Tables;
            Date.BlackoutDates.AddDatesInPast();
        }
        
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtClientName.Text != ""
                || txtSeats.Text != ""
                || txtPhone.Text != "")
                {

                    int NameofTable = Combobox_number_of_Table.SelectedIndex;

                    switch (NameofTable)
                    {
                        case 0:
                            NameofTable = 1;
                            break;
                        case 1:
                            NameofTable = 2;
                            break;
                        case 2:
                            NameofTable = 3;
                            break;
                        case 3:
                            NameofTable = 4;
                            break;
                        case 4:
                            NameofTable = 5;
                            break;
                        case 5:
                            NameofTable = 6;
                            break;
                        case 6:
                            NameofTable = 7;
                            break;
                    }

                    string ClientName = txtClientName.Text;
                    int Seats = int.Parse(txtSeats.Text);
                    string ClientPhoneNumber = txtPhone.Text;
                    int UpdatedBy = _UsersService.CurrentUser.Id;
                    var date = Date.SelectedDate.Value;
                    var time = Time.Value.Value;
                    var dateTimeOffSet = new DateTimeOffset(
                        date.Year,
                        date.Month,
                        date.Day,
                        time.Hour,
                        time.Minute,
                        0,
                        TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now)
                        );
                    if (dateTimeOffSet.Hour < 8)
                    {
                        throw new Exception("Ресторан еще закрыт");
                    }
                    else
                    {
                        _BookingsService.Add_Booking(ClientName, dateTimeOffSet, Seats, (Tables)NameofTable, ClientPhoneNumber, UpdatedBy, DateTimeOffset.Now);
                        ListViewTables.Items.Refresh();
                        ListViewTables.ItemsSource = _BookingsService.GetfreeTables(new DateTimeOffset(dateTimeOffSet.Date));
                        ListViewBookings.ItemsSource = _BookingsService.GGetBookedTablesByDate(new DateTimeOffset(dateTimeOffSet.Date));

                        txtClientName.Clear();
                        Date.SelectedDate = null;
                        Time.Text = null;
                        txtSeats.Clear();
                        txtPhone.Clear();
                        Combobox_number_of_Table.SelectedIndex = -1;

                    }
                }
                else
                {
                    throw new Exception("Заполните все поля");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Can't add booking: '{ex.Message}'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Date_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                var date = Date.SelectedDate;

                if (date.HasValue)
                {
                    ListViewBookings.ItemsSource = _BookingsService.GGetBookedTablesByDate(new DateTimeOffset(date.Value));
                    ListViewTables.Items.Refresh();
                    ListViewTables.ItemsSource = _BookingsService.GetfreeTables(new DateTimeOffset(date.Value));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Can't add booking: '{ex.Message}'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Date.SelectedDate = null;
            }
        }

        private void Time_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex(@"[0-9/A-z*a-z/+]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Date_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex(@"[0-9/A-z*a-z]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtSeats_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex(@"[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtPhone_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[^+]*[^0-9+]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtClientName_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex(@"[^A-z*a-z/а-я/А-Я]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}

