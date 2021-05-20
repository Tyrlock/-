namespace CoreLibarary.Services
{
    using System;
    using System.Collections.ObjectModel;

    using CoreLibarary.Interfaces;
    using CoreLibarary.Models;



    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _MenuRepository;

        private ObservableCollection<Dish> _Menu;

        public ObservableCollection<Dish> Menu
        {
            get
            {
                return _Menu;
            }
        }

        public MenuService(IMenuRepository menuRepository)
        {
            _MenuRepository = menuRepository;
            _Menu = Get();
        }

        public ObservableCollection<Dish> Get()
        {
            var menu = new ObservableCollection<Dish>(_MenuRepository.Get());
            return menu;
        }

        public int? Add(string productName,
            int cost,
            string description,
            int categoryId,
            int updatedBy,
            DateTimeOffset updatedDate,
            byte[] imageBytes)
        {
            if (_MenuRepository.IsDishExists(productName))
            {
                throw new Exception("Даный пункт меню уже существует");
            }
            var dishId = _MenuRepository.Add(productName, cost, description, categoryId, updatedBy, updatedDate, imageBytes);
            if (dishId.HasValue && dishId >= 0)
            {
                Menu.Add(new Dish
                {
                    ProductName = productName,
                    Cost = cost,
                    Description = description,
                    CategoryId = (Category)categoryId,
                    UpdatedBy = updatedBy,
                    UpdatedDate= updatedDate,
                    image= imageBytes
                });
            }

            return dishId;
        }

        public bool Update(Dish menu)
        {
            return _MenuRepository.Update(menu);
        }

        public bool Delete(int menuId)
        {
            return _MenuRepository.Delete(menuId);
        }
    }
}
