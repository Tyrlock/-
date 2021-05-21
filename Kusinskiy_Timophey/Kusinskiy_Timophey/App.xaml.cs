using System.Data.SqlClient;
using System.Windows;

using CoreLibarary.Interfaces;
using CoreLibarary.Repositories;
using CoreLibarary.Services;

namespace Kusinskiy_Timophey
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow MainWindow;
        IUsersService _UsersService { get; set; }
        IMenuService _MenusService { get; set; }
        IBookingsService _BookingsService { get; set; }
        IReviewsService _ReviewsService { get; set; }

        IUsersRepository _UsersRepository { get; set; }
        IMenuRepository _MenuRepository { get; set; }
        IBookingsRepository _BookingsRepository { get; set; }
        IReviewsRepository _ReviewsRepository { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
                _UsersRepository = new SQLUsersRepository();
                _MenuRepository = new SQLMenuRepository();
                _BookingsRepository = new SQLBookingsRepository();
                _ReviewsRepository = new SQLReviewsRepository();

                _UsersService = new UsersService(_UsersRepository);
                _MenusService = new MenuService(_MenuRepository);
                _BookingsService = new BookingsService(_BookingsRepository);
                _ReviewsService = new ReviewsService(_ReviewsRepository);

            MainWindow = new MainWindow(_UsersService, _MenusService, _BookingsService, _ReviewsService);

            var loginForm = new LoginForm(_UsersService);
            var result_log = loginForm.ShowDialog();

            if (result_log.HasValue && result_log.Value
                && _UsersService.CurrentUser != null)
            {
                // Создание главного окна приложения, стартуя с логина
                MainWindow.Show();
                App.Current.MainWindow = null;
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}
