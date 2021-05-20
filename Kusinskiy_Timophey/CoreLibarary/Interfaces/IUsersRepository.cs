using CoreLibarary.Models;
using System.Collections.ObjectModel;

namespace CoreLibarary.Interfaces
{
    public interface IUsersRepository
    {
        int? Create(
            string name,
            string phone,
            string email,
            string password,
            int roleId);

        bool Delete(int userId);

        bool Update(User user);

        User Get(int userId);

        User Get(string name);

        ObservableCollection<User> Get();

        bool IsAdminExists();
        bool IsUserExists(string userName);
    }
}
