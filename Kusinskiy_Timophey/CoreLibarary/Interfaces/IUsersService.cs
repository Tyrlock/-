namespace CoreLibarary.Interfaces
{
    using System.Collections.ObjectModel;
    using CoreLibarary.Models;


    public interface IUsersService
    {
        User CurrentUser { get; }

        int? Create(
            string name,
            string phone,
            string email,
            string password,
            int roleId);
        bool Delete(int userId);

        bool Update(User user);

        User Get(int userId);

        ObservableCollection<User> Get();

        bool Login(string name, string password);
    }

}
