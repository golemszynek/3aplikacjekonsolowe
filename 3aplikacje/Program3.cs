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
