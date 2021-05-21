using CoreLibarary.Interfaces;
using CoreLibarary.Models;
using System.Collections.ObjectModel;
using System;
using System.Text.RegularExpressions;

namespace CoreLibarary.Services
{
    public class BookingsService : IBookingsService
    {
        private readonly IBookingsRepository _BookingsRepository;

        private ObservableCollection<Booking> _Bookings;

        private ObservableCollection<Table> _Tables;

        public ObservableCollection<Booking> Bookings
        {
            get
            {
                return _Bookings;
            }
        }

        public ObservableCollection<Table> Tables
        {
            get
            {
                return _Tables;
            }
        }

        public ObservableCollection<Table> GetfreeTables(DateTimeOffset date)
        {
            _Tables = GetTables();
            ObservableCollection<Table> list = Tables;
            DateTimeOffset dateforecheck = new DateTimeOffset(
                        date.Year,
                        date.Month,
                        date.Day,
                        0,
                        0,
                        0,
                        TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now)
                        );
            ObservableCollection<Booking> bookings = new ObservableCollection<Booking>(_BookingsRepository.GGetBookedTablesByDate(dateforecheck));

            for (int i = 0; i < bookings.Count; i++)
            {
                for (int t = 0; t < _Tables.Count; t++)
                {
                    if ((int)bookings[i].TableId == _Tables[t].Id)
                    {
                        if (_Tables[t].TotalSeats - bookings[i].Seats < 0)
                        {
                            throw new Exception("Все места заняты");
                        }
                        else
                        {
                            list[t].TotalSeats = _Tables[t].TotalSeats - bookings[i].Seats;
                        }
                    }
                }
            }
                return list;
        }

        public BookingsService(IBookingsRepository bookingsRepository)
        {
            _BookingsRepository = bookingsRepository;

            _Bookings = Get();

            _Tables = GetTables();
        }

        public ObservableCollection<Table> GetTables()
        {
            ObservableCollection<Table> list = new ObservableCollection<Table>(_BookingsRepository.GetTables());

            return list;
        }

        public ObservableCollection<Booking> Get()
        {
            ObservableCollection<Booking> list = new ObservableCollection<Booking>(_BookingsRepository.Get());

            return list;
        }

        public int? Add_Booking(string clientName,
            DateTimeOffset dataTimeBooking,
            int seats,
            Tables tableId,
            string clientPhoneNumber,
            int updatedBy,
            DateTimeOffset updatedDate)
        {
            var list = GetfreeTables(dataTimeBooking);

            for (int t = 0; t < _Tables.Count; t++)
            {
                if ((int)tableId == _Tables[t].Id)
                {
                    if (_Tables[t].TotalSeats - seats < 0)
                    {
                        throw new Exception("Все места заняты");
                    }
                    else
                    {
                        list[t].TotalSeats = _Tables[t].TotalSeats - seats;
                        //Проверка phone_number
                        if (!Regex.Match(clientPhoneNumber, @"^(\+[3][7][5][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9])*$").Success)
                        {
                            throw new Exception("Номер телефона введен некорректно!");
                        }

                        var bookingId = _BookingsRepository.Add_Booking(clientName, dataTimeBooking, seats, tableId, clientPhoneNumber, updatedBy, updatedDate);

                        if (bookingId.HasValue && bookingId >= 0)
                        {
                            Bookings.Add(new Booking
                            {
                                ClientName = clientName,
                                DataTimeBooking = dataTimeBooking,
                                Seats = seats,
                                TableId = tableId,
                                ClientPhoneNumber = clientPhoneNumber,
                                UpdatedBy = updatedBy,
                                UpdatedDate = updatedDate
                            });
                        }
                        return bookingId;
                    }
                }
            }
            throw new Exception("Не удалось забронировать столик");
        }

        public ObservableCollection<Booking> GGetBookedTablesByDate(DateTimeOffset date)
        {
            ObservableCollection<Booking> list = new ObservableCollection<Booking>(_BookingsRepository.GGetBookedTablesByDate(date));

            return list;
        }
    }
}
