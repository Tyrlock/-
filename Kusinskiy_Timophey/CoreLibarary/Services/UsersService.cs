
namespace CoreLibarary.Services
{
    using System;
    using System.Collections.ObjectModel;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using CoreLibarary.Interfaces;
    using CoreLibarary.Models;

    /// <summary>
    /// Users Service implementation
    /// </summary>
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _UsersRepository;

        private User _CurrenrtUser;

        private HashAlgorithm _HashAlgorithm = new MD5CryptoServiceProvider();

        public User CurrentUser
        {
            get
            {
                return _CurrenrtUser;
            }
        }

        public UsersService(IUsersRepository usersRepository)
        {
            _UsersRepository = usersRepository;
        }

        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="name"> Имя</param>
        /// <param name="phone"> Номер телефона</param>
        /// <param name="email"> Емэйл </param>
        /// <param name="password"> Пароль </param>
        /// <param name="roleId"> Роль </param>
        /// <returns></returns>
        public int? Create(
            string name,
            string phone,
            string email,
            string password,
            int roleId)
        {
            //Проверка роли
            if (_UsersRepository.IsUserExists(name))
            {
                throw new Exception("Даный пользователь уже существует");
            }

            //Проверка phone_number
            if (!Regex.Match(phone, @"^(\+[3][7][5][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9])*$").Success)
            {
                throw new Exception("Номер телефона введен некорректно!");
            }

            //Проверка email
            var emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                throw new Exception("Неверный формат эл.почты!");
            }

            //Проверка роли
            if (roleId == 1
                && _UsersRepository.IsAdminExists())
            {
                throw new Exception("У вас нет разрешения на создание пользователя с правами администратора");
            }

            password = _GetPasswordHash(password, _HashAlgorithm);

            return _UsersRepository.Create(name, phone, email, password, roleId);
        }


        private string _GetPasswordHash(string password, HashAlgorithm algorithm)
        {
            var inputBytes = Encoding.UTF8.GetBytes(password);
            //Вычисляет хэш-значение для заданного массива байтов.
            var hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }


        /// <summary>
        /// Удаление пользователей
        /// </summary>
        /// <param name="userId"> ID пользователя</param>
        /// <returns></returns>
        public bool Delete(int userId)
        {
                if (userId == CurrentUser.Id)
                {
                    throw new Exception("Вы не можете удалить самого себя");
                }
                return _UsersRepository.Delete(userId);
        }

        /// <summary>
        /// Возврат пользователя по userID 
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <returns></returns>
        public User Get(int userId)
        {
            return new User();
        }

        /// <summary>
        /// Список пользователей
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<User> Get()
        {
            ObservableCollection<User> list = new ObservableCollection<User>(_UsersRepository.Get());
            return list;
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="name"> Имя пользователя </param>
        /// <param name="password"> Пароль </param>
        /// <returns></returns>
        public bool Login(string name, string password)
        {
            var user = _UsersRepository.Get(name);
            if (user !=null)
            {
                password = _GetPasswordHash(password, _HashAlgorithm);
                if (user.Password == password)
                {
                    _CurrenrtUser = user;
                    return true;
                }
                else
                {
                    throw new Exception("Введен неверный пароль");
                }
            }
            return false;
        }

        /// <summary>
        /// Изменение пользователей
        /// </summary>
        /// <param name="user"> Обьект класса User </param>
        /// <returns></returns>
        public bool Update(User user)
        {
            //Проверка phone_number
            if (!Regex.Match(user.Phone, @"^(\+[3][7][5][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9])*$").Success)
            {
                throw new Exception("Номер телефона введен некорректно!");
            }

            //Проверка email
            var emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            if (!Regex.IsMatch(user.Email, emailPattern))
            {
                throw new Exception("Неверный формат эл.почты!");
            }
            if (user.Id==CurrentUser.Id)
            {
                throw new Exception("Вы не можете изменять самого себя");
            }
            else
            {
                return _UsersRepository.Update(user);
            }
        }
    }
}
