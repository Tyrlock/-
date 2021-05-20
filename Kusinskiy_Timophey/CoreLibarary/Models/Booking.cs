using System;

namespace CoreLibarary.Models
{

    public enum Tables
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Fifth = 5,
        Sixth=6,
        Seventh=7,
    }
    public class Booking
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public DateTimeOffset DataTimeBooking { get; set; }
        public int Seats { get; set; }
        public Tables TableId { get; set; }
        public string ClientPhoneNumber { get; set; }
        public int UpdatedBy { get; set; }
        public string Number_of_table { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
