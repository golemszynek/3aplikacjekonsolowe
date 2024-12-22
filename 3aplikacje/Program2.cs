
using System;
using System.Collections.Generic;
using System.Linq;

public class Concert
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public int AvailableSeats { get; set; }

    public Concert(string name, DateTime date, string location, int availableSeats)
    {
        Name = name;
        Date = date;
        Location = location;
        AvailableSeats = availableSeats;
    }
}

public class Ticket
{
    public Concert Concert { get; set; }
    public decimal Price { get; set; }
    public int SeatNumber { get; set; }

    public Ticket(Concert concert, decimal price, int seatNumber)
    {
        Concert = concert;
        Price = price;
        SeatNumber = seatNumber;
    }
}

public interface IConcertType
{
    string GetConcertType();
}

public class RegularConcert : IConcertType
{
    public string GetConcertType() => "Regular Concert";
}

public class VIPConcert : IConcertType
{
    public string GetConcertType() => "VIP Concert";
}

public class OnlineConcert : IConcertType
{
    public string GetConcertType() => "Online Concert";
}

public class PrivateConcert : IConcertType
{
    public string GetConcertType() => "Private Concert";
}
public class BookingSystem
{
    private List<Concert> concerts = new List<Concert>();
    private List<Ticket> tickets = new List<Ticket>();

    public void AddConcert(Concert concert)
    {
        concerts.Add(concert);
    }

    public Ticket BookTicket(Concert concert, decimal price, bool isVIP = false)
    {
        if (concert.AvailableSeats > 0)
        {
            concert.AvailableSeats--;
            var ticket = new Ticket(concert, price, concert.AvailableSeats + 1);
            tickets.Add(ticket);
            return ticket;
        }
        return null;
    }

    public List<Concert> GetConcertsByDate(DateTime date)
    {
        return concerts.Where(c => c.Date.Date == date.Date).ToList();
    }

    public List<Concert> GetConcertsByLocation(string location)
    {
        return concerts.Where(c => c.Location.Equals(location, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Concert> FilterConcerts(Func<Concert, bool> criteria)
    {
        return concerts.Where(criteria).ToList();
    }

    public void GenerateReport()
    {
        var report = tickets.GroupBy(t => t.Concert.Name)
                            .Select(g => new { ConcertName = g.Key, SoldTickets = g.Count() })
                            .ToList();
        foreach (var item in report)
        {
            Console.WriteLine($"Concert: {item.ConcertName}, Sold Tickets: {item.SoldTickets}");
        }
    }

    public void NotifyLowAvailability(int threshold)
    {
        var lowAvailabilityConcerts = concerts.Where(c => c.AvailableSeats < threshold).ToList();
        foreach (var concert in lowAvailabilityConcerts)
        {
            Console.WriteLine($"Concert: {concert.Name} has low ticket availability: {concert.AvailableSeats} seats left.");
        }
    }

    public void CancelReservation(Ticket ticket)
    {
        if (tickets.Remove(ticket))
        {
            ticket.Concert.AvailableSeats++;
        }
    }
}

public class Program
{
    public static void Main()
    {
        BookingSystem bookingSystem = new BookingSystem();

        Concert concert1 = new Concert("Rock Night", new DateTime(2024, 12, 25), "Warsaw", 100);
        Concert concert2 = new Concert("Jazz Evening", new DateTime(2024, 12, 24), "Krakow", 50);
        Concert concert3 = new Concert("VIP Gala", new DateTime(2024, 12, 31), "Warsaw", 20);

        bookingSystem.AddConcert(concert1);
        bookingSystem.AddConcert(concert2);
        bookingSystem.AddConcert(concert3);

        var ticket1 = bookingSystem.BookTicket(concert1, 150.00m);
        var ticket2 = bookingSystem.BookTicket(concert2, 120.00m);
        var ticket3 = bookingSystem.BookTicket(concert3, 300.00m, true);

        var concertsByDate = bookingSystem.GetConcertsByDate(new DateTime(2024, 12, 25));
        Console.WriteLine($"Concerts on 2024-12-25: {string.Join(", ", concertsByDate.Select(c => c.Name))}");

        var concertsByLocation = bookingSystem.GetConcertsByLocation("Krakow");
        Console.WriteLine($"Concerts in Krakow: {string.Join(", ", concertsByLocation.Select(c => c.Name))}");

        var filteredConcerts = bookingSystem.FilterConcerts(c => c.Date.Year == 2024 && c.Location == "Warsaw");
        Console.WriteLine($"Filtered Concerts: {string.Join(", ", filteredConcerts.Select(c => c.Name))}");

        bookingSystem.GenerateReport();

        bookingSystem.NotifyLowAvailability(10);

        bookingSystem.CancelReservation(ticket3);
        Console.WriteLine($"Reservation cancelled for concert: {ticket3.Concert.Name}, seat number: {ticket3.SeatNumber}");

        bookingSystem.GenerateReport();
    }
}
