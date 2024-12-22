using System;
using System.Collections.Generic;

namespace BoardGameSimulator
{
    public class Player
    {
        public string Name { get; set; }
        public int Position { get; set; }
        public int Score { get; set; }
        public IPlayerType PlayerType { get; set; }

        public Player(string name, IPlayerType playerType)
        {
            Name = name;
            Position = 0;
            Score = 0;
            PlayerType = playerType;
        }

        public void Move(int steps)
        {
            Position += steps;
            Console.WriteLine($"{Name} przesunął się o {steps} pól, aktualna pozycja: {Position}");
        }

        public void UpdateScore(int points)
        {
            Score += points;
            Console.WriteLine($"{Name} zdobył {points} punktów, aktualny wynik: {Score}");
        }

        public void UseSpecialAbility()
        {
            PlayerType.SpecialAbility(this);
        }
    }

    public interface IPlayerType
    {
        void SpecialAbility(Player player);
    }

    public class Warrior : IPlayerType
    {
        public void SpecialAbility(Player player)
        {
            player.UpdateScore(10);
            Console.WriteLine($"{player.Name} zdobył dodatkowe punkty jako Wojownik!");
        }
    }

    public class Mage : IPlayerType
    {
        public void SpecialAbility(Player player)
        {
            player.Move(2);
            Console.WriteLine($"{player.Name} rzucił zaklęcie i przesunął się o dodatkowe pola jako Mag!");
        }
    }

    public class Healer : IPlayerType
    {
        public void SpecialAbility(Player player)
        {
            player.UpdateScore(5);
            Console.WriteLine($"{player.Name} leczy innych i zdobywa punkty jako Healer!");
        }
    }

}