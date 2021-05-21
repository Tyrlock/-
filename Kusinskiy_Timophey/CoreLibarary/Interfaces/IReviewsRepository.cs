using CoreLibarary.Models;
using System.Collections.ObjectModel;

namespace CoreLibarary.Interfaces
{
    public interface IReviewsRepository
    {
        int? Create(
            int creatidBy,
            string description,
            int name);

        ObservableCollection<Reviews> Get();

    }
}
