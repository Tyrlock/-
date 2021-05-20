using CoreLibarary.Models;
using System.Collections.ObjectModel;
using System;

namespace CoreLibarary.Interfaces
{
    public interface IBookingsService
    {
        ObservableCollection<Booking> Bookings { get; }

        ObservableCollection<Booking> Get();

        ObservableCollection<Table> Tables { get; }

        ObservableCollection<Table> GetTables();

        ObservableCollection<Table> GetfreeTables(DateTimeOffset date);


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
