namespace CoreLibarary.Models
{
    public class Reviews
    {
        public int Id { get; set; }

        public int CreatedBy { get; set; }

        public string Description { get; set; }

        public int RecepientId { get; set; }

        public string RecepientName { get; set; }

        public string CreatedByName { get; set; }
    }
}