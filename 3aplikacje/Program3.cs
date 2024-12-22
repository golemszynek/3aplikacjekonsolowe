using System;
using System.Collections.Generic;
using System.Linq;

public class Player
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public int Score { get; set; }

    public Player(string firstName, string lastName, string position, int score)
    {
        FirstName = firstName;
        LastName = lastName;
        Position = position;
        Score = score;
    }

    public void UpdateScore(int newScore)
    {
        Score = newScore;
    }
}

public class Team
{
    private List<Player> players = new List<Player>();

    public void AddPlayer(Player player)
    {
        if (string.IsNullOrWhiteSpace(player.FirstName) || string.IsNullOrWhiteSpace(player.LastName) ||
            string.IsNullOrWhiteSpace(player.Position))
        {
            Console.WriteLine("Zawodnik musi mieć przypisane imię, nazwisko i pozycję.");
            return;
        }
        players.Add(player);
    }

    public void RemovePlayer(string firstName, string lastName)
    {
        players.RemoveAll(p => p.FirstName == firstName && p.LastName == lastName);
    }

    public List<Player> GetPlayersByPosition(string position)
    {
        return players.Where(p => p.Position.Equals(position, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public double CalculateAverageScore()
    {
        return players.Count > 0 ? players.Average(p => p.Score) : 0;
    }

    public void DisplayTeamStats()
    {
        foreach (var player in players)
        {
            Console.WriteLine($"FirstName: {player.FirstName}, LastName: {player.LastName}, Position: {player.Position}, Score: {player.Score}");
        }
    }

    public List<Player> FilterPlayers(Func<Player, bool> criteria)
    {
        return players.Where(criteria).ToList();
    }
}

public interface IPlayerType
{
    string GetPlayerType();
}

public class Defender : IPlayerType
{
    public string GetPlayerType() => "Defender";
}

public class Striker : IPlayerType
{
    public string GetPlayerType() => "Striker";
}

public class Goalkeeper : IPlayerType
{
    public string GetPlayerType() => "Goalkeeper";
}

public class Midfielder : IPlayerType
{
    public string GetPlayerType() => "Midfielder";
}

public class Program
{
    public static void Main()
    {
        Team team = new Team();

        while (true)
        {
            Console.WriteLine("Wybierz operację:");
            Console.WriteLine("1. Dodaj zawodnika");
            Console.WriteLine("2. Usuń zawodnika");
            Console.WriteLine("3. Aktualizuj wynik zawodnika");
            Console.WriteLine("4. Wyświetl statystyki drużyny");
            Console.WriteLine("5. Oblicz średnią punktów drużyny");
            Console.WriteLine("6. Wyszukaj zawodników według pozycji");
            Console.WriteLine("7. Filtrowanie zawodników na podstawie kryteriów");
            Console.WriteLine("8. Wyjście");
            Console.Write("Twój wybór: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Imię: ");
                    string firstName = Console.ReadLine();
                    Console.Write("Nazwisko: ");
                    string lastName = Console.ReadLine();
                    Console.Write("Pozycja: ");
                    string position = Console.ReadLine();
                    Console.Write("Wynik: ");
                    int score = int.Parse(Console.ReadLine());
                    team.AddPlayer(new Player(firstName, lastName, position, score));
                    break;

                case 2:
                    Console.Write("Imię: ");
                    string firstNameToRemove = Console.ReadLine();
                    Console.Write("Nazwisko: ");
                    string lastNameToRemove = Console.ReadLine();
                    team.RemovePlayer(firstNameToRemove, lastNameToRemove);
                    break;

                case 3:
                    Console.Write("Imię: ");
                    string firstNameToUpdate = Console.ReadLine();
                    Console.Write("Nazwisko: ");
                    string lastNameToUpdate = Console.ReadLine();
                    Console.Write("Nowy wynik: ");
                    int newScore = int.Parse(Console.ReadLine());
                    var playerToUpdate = team.FilterPlayers(p => p.FirstName.Equals(firstNameToUpdate, StringComparison.OrdinalIgnoreCase) && p.LastName.Equals(lastNameToUpdate, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    if (playerToUpdate != null)
                    {
                        playerToUpdate.UpdateScore(newScore);
                    }
                    break;

                case 4:
                    team.DisplayTeamStats();
                    break;

                case 5:
                    double averageScore = team.CalculateAverageScore();
                    Console.WriteLine($"Średnia punktów drużyny: {averageScore}");
                    break;

                case 6:
                    Console.Write("Pozycja: ");
                    string positionToSearch = Console.ReadLine();
                    var playersByPosition = team.GetPlayersByPosition(positionToSearch);
                    Console.WriteLine($"Zawodnicy na pozycji {positionToSearch}:");
                    foreach (var player in playersByPosition)
                    {
                        Console.WriteLine($"FirstName: {player.FirstName}, LastName: {player.LastName}, Score: {player.Score}");
                    }
                    break;

                case 7:
                    Console.WriteLine("Filtrowanie zawodników z wynikiem powyżej 80:");
                    var highScorers = team.FilterPlayers(p => p.Score > 80);
                    foreach (var player in highScorers)
                    {
                        Console.WriteLine($"FirstName: {player.FirstName}, LastName: {player.LastName}, Position: {player.Position}, Score: {player.Score}");
                    }
                    break;

                case 8:
                    return;

                default:
                    Console.WriteLine("Niepoprawny wybór, spróbuj ponownie.");
                    break;
            }
        }
    }
}