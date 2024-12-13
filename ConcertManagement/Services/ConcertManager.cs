using ConcertManagement.Models;

namespace ConcertManagement.Services
{
    public static class ConcertManager
    {
        private static List<Concert> concerts = new List<Concert>();

        public static void AddConcert()
        {
            Console.Write("Enter location: ");
            string? location = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(location))
            {
                Console.WriteLine("Invalid location.");
                return;
            }

            Console.Write("Enter performer/ensemble: ");
            string? performer = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(performer))
            {
                Console.WriteLine("Invalid performer.");
                return;
            }

            Console.Write("Enter date and time (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dateAndTime))
            {
                Console.WriteLine("Invalid date and time format.");
                return;
            }

            Console.Write("Enter capacity: ");
            if (!int.TryParse(Console.ReadLine(), out int capacity) || capacity <= 0)
            {
                Console.WriteLine("Invalid capacity.");
                return;
            }

            Console.Write("Enter price: ");
            if (!double.TryParse(Console.ReadLine(), out double price) || price < 0)
            {
                Console.WriteLine("Invalid price.");
                return;
            }

            int newId = concerts.Any() ? concerts.Max(c => c.Id) + 1 : 1;
            concerts.Add(new Concert
            {
                Id = newId,
                Location = location,
                Performer = performer,
                DateAndTime = dateAndTime,
                Capacity = capacity,
                Price = price
            });

            Console.WriteLine("Concert added successfully!");
        }

        public static void ListConcerts()
        {
            if (concerts.Count == 0)
            {
                Console.WriteLine("No concerts available.");
                return;
            }

            Console.WriteLine("Concerts list: ");
            foreach (var concert in concerts)
            {
                Console.WriteLine(concert);
            }
        }

        public static void EditConcert()
        {
            Console.Write("Enter the ID of the concert to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var concert = concerts.FirstOrDefault(c => c.Id == id);
            if (concert == null)
            {
                Console.WriteLine("Concert not found.");
                return;
            }

            Console.Write($"Enter new Location (current: {concert.Location}): ");
            string? location = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(location)) concert.Location = location;

            Console.Write($"Enter new performer (current: {concert.Performer}): ");
            string? performer = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(performer)) concert.Performer = performer;

            Console.WriteLine($"Enter new date and time (current: {concert.DateAndTime:yyyy-MM-dd HH:mm}): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dateAndTime)) concert.DateAndTime = dateAndTime;

            Console.Write($"Enter new capacity (current: {concert.Capacity}): ");
            if (int.TryParse(Console.ReadLine(), out int capacity) && capacity > 0) concert.Capacity = capacity;

            Console.Write($"Enter new price (current: {concert.Price:C}): ");
            if (double.TryParse(Console.ReadLine(), out double price) && price >= 0) concert.Price = price;

            Console.WriteLine("Concert updated successfully!");
        }

        public static void DeleteConcert()
        {
            Console.Write("Enter the ID of the concert to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var concert = concerts.FirstOrDefault(c => c.Id == id);
            if (concert == null)
            {
                Console.WriteLine("Concert not found.");
                return;
            }

            concerts.Remove(concert);
            Console.WriteLine("Concert deleted successfully!");
        }

        public static void SaveToFile()
        {
            using (var writer = new StreamWriter("Concerts.txt"))
            {
                foreach (var concert in concerts)
                {
                    writer.WriteLine($"{concert.Id}|{concert.Location}|{concert.Performer}|{concert.DateAndTime}|{concert.Capacity}|{concert.Price}");
                }
            }

            Console.WriteLine("Data saved!");
        }

        public static void LoadFromFile()
        {
            if (!File.Exists("Concerts.txt"))
            {
                Console.WriteLine("No saved concerts found.");
                return;
            }

            concerts.Clear();
            foreach (var line in File.ReadAllLines("Concerts.txt"))
            {
                var parts = line.Split('|');
                if (parts.Length < 6) continue;

                if (int.TryParse(parts[0], out int id) &&
                    DateTime.TryParse(parts[3], out DateTime dateAndTime) &&
                    int.TryParse(parts[4], out int capacity) &&
                    double.TryParse(parts[5], out double price))
                {
                    concerts.Add(new Concert
                    {
                        Id = id,
                        Location = parts[1],
                        Performer = parts[2],
                        DateAndTime = dateAndTime,
                        Capacity = capacity,
                        Price = price
                    });
                }
            }
        }

        public static List<Concert> GetConcerts() => concerts;

        public static void AddConcertFromApi(Concert concert)
        {
            if (concert == null)
            {
                throw new ArgumentNullException(nameof(concert), "Concert cannot be null");
            }

            concert.Id = concerts.Any() ? concerts.Max(c => c.Id) + 1 : 1;
            concerts.Add(concert);
        }
    }
}