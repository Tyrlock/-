using CoreLibarary.Models;
using System;
using System.Collections.ObjectModel;

namespace CoreLibarary.Interfaces
{
    public interface IMenuRepository
    {

        int? Add(string productName,
            int cost,
            string description,
            int categoryId,
            int updatedBy,
            DateTimeOffset updatedDate,
            byte[] imageBytes);
        ObservableCollection<Dish> Get();
        bool Update(Dish menu);
        bool Delete(int menuId);
        bool IsDishExists(string dishname);
    }
}
