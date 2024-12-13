namespace ConcertManagement.Models
{
    public class Concert
    {
        public int Id { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Performer { get; set; } = string.Empty;
        public DateTime DateAndTime { get; set; }
        public int Capacity { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Performer} at {Location} on {DateAndTime:yyyy-MM-dd HH:mm}. Capacity: {Capacity}. Price: {Price:0.00} €";
        }
    }
}