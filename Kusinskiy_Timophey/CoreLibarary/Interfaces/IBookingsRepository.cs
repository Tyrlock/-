using CoreLibarary.Models;
using System;
using System.Collections.ObjectModel;

namespace CoreLibarary.Interfaces
{
    public interface IBookingsRepository
    {

        ObservableCollection<Booking> Get();

        ObservableCollection<Table> GetTables();

        ObservableCollection<Booking> GGetBookedTablesByDate(DateTimeOffset date);

        int? Add_Booking(string ClientName,
            DateTimeOffset DataTimeBooking,
            int Seats,
            Tables TableId,
            string ClientPhoneNumber,
            int UpdatedBy,
            DateTimeOffset UpdatedDate);
    }
}
