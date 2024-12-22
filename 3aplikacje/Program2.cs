
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
