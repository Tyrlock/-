using CoreLibarary.Models;
using System.Collections.ObjectModel;

namespace CoreLibarary.Interfaces
{
    public interface IReviewsService
    {
        int? Create(
            int creatidBy,
            string description,
            int name);

        ObservableCollection<Reviews> Reviews { get; }

        ObservableCollection<Reviews> Get();


    }
}
