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

    public class Board
    {
        private int size;
        private Dictionary<int, int> rewards;

        public Board(int size)
        {
            this.size = size;
            rewards = new Dictionary<int, int>();
            GenerateRewards();
        }

        private void GenerateRewards()
        {
            Random rand = new Random();
            for (int i = 0; i < size / 2; i++)
            {
                int position = rand.Next(1, size);
                int points = rand.Next(1, 11);
                rewards[position] = points;
            }
        }

        public int CheckReward(int position)
        {
            return rewards.ContainsKey(position) ? rewards[position] : 0;
        }

        public int Size => size;
    }

    public class Game
    {
        private Board board;
        private List<Player> players;
        private Random rand;

        public Game(Board board, List<Player> players)
        {
            this.board = board;
            this.players = players;
            rand = new Random();
        }

        public void Start()
        {
            bool gameOver = false;
            while (!gameOver)
            {
                foreach (var player in players)
                {
                    PlayTurn(player);
                    if (player.Position >= board.Size)
                    {
                        gameOver = true;
                        Console.WriteLine($"{player.Name} wygrał grę!");
                        break;
                    }
                }
            }

            DisplayResults();
        }

        private void PlayTurn(Player player)
        {
            Console.WriteLine($"{player.Name}'s turn.");
            int roll = rand.Next(1, 7);
            Console.WriteLine($"{player.Name} rzucił {roll}.");

            player.Move(roll);

            int reward = board.CheckReward(player.Position);
            if (reward > 0)
            {
                player.UpdateScore(reward);
                Console.WriteLine($"{player.Name} znalazł nagrodę: {reward} punktów!");
            }

            player.UseSpecialAbility();

            Console.WriteLine($"{player.Name} kończy turę.\n");
        }

        private void DisplayResults()
        {
            Console.WriteLine("Koniec gry! Wyniki:");
            foreach (var player in players)
            {
                Console.WriteLine($"{player.Name}: {player.Score} punktów");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(20);

            Player player1 = new Player("Kasia", new Warrior());
            Player player2 = new Player("Wojtek", new Mage());

            List<Player> players = new List<Player> { player1, player2 };

            Game game = new Game(board, players);
            game.Start();
        }
    }
}