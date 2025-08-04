using System;
using System.Collections.Generic;
using System.Threading;

namespace CleanCasinoProject
{
    public class Card
    {
        public string Suit { get; }
        public string Rank { get; }
        public int Value { get; }

        public Card(string suit, string rank, int value)
        {
            Suit = suit;
            Rank = rank;
            Value = value;
        }

        public override string ToString()
        {
            string symbol = Suit switch
            {
                "Hearts" => "♥",
                "Diamonds" => "♦",
                "Clubs" => "♣",
                "Spades" => "♠",
                _ => "?"
            };
            return $"{Rank}{symbol}";
        }
    }

    public class Deck
    {
        private readonly List<Card> cards;
        private readonly Random random = new();

        public Deck()
        {
            cards = new List<Card>();
            InitializeDeck();
            Shuffle();
        }

        private void InitializeDeck()
        {
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            int[] values = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };

            foreach (var suit in suits)
            {
                for (int i = 0; i < ranks.Length; i++)
                {
                    cards.Add(new Card(suit, ranks[i], values[i]));
                }
            }
        }

        public void Shuffle()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                int j = random.Next(i, cards.Count);
                (cards[i], cards[j]) = (cards[j], cards[i]);
            }
        }

        public Card DrawCard()
        {
            if (cards.Count == 0)
                throw new InvalidOperationException("The deck is empty!");
            Card drawn = cards[0];
            cards.RemoveAt(0);
            return drawn;
        }
    }

    public class Player
    {
        public string Name { get; }
        public List<Card> Hand { get; }
        public bool IsDealer { get; }
        public int Money { get; set; }
        public int Bet { get; set; }

        public Player(string name, bool isDealer = false, int initialMoney = 1500)
        {
            Name = name;
            IsDealer = isDealer;
            Money = initialMoney;
            Hand = new List<Card>();
        }

        public void AddCard(Card card) => Hand.Add(card);
        public void ClearHand() => Hand.Clear();

        public int GetScore()
        {
            int score = 0;
            int aceCount = 0;

            foreach (var card in Hand)
            {
                score += card.Value;
                if (card.Rank == "A") aceCount++;
            }

            while (score > 21 && aceCount-- > 0)
                score -= 10;

            return score;
        }

        public void ShowHand()
        {
            Console.ForegroundColor = IsDealer ? ConsoleColor.Cyan : ConsoleColor.Green;
            Console.WriteLine($"\n{(IsDealer ? "🃏 Dealer" : Name)} Hand:");
            Console.ResetColor();

            foreach (var card in Hand)
            {
                Console.WriteLine($"  → {card}");
            }

            Console.WriteLine($"Total: {GetScore()}\n");
        }
    }

    public class SlotMachine
    {
        private static readonly string[] symbols = { "🍒", "💎", "🔔", "7️⃣" };
        private static readonly string[] spinSymbols = { "🍒", "💎", "🔔", "7️⃣", "💰", "🍋", "⭐", "🃏" };
        private static readonly Random rng = new();
        public static int konto = 1500;
        private const int spinCost = 10;

        public static void Start()
        {
            Console.Clear();
            Console.WriteLine("🎰 Slot-Spiel gestartet!");
            Console.WriteLine($"Jeder Spin kostet {spinCost} Jetons.");

            while (konto >= spinCost)
            {
                Console.WriteLine($"\n💰 Guthaben: {konto} Jetons");
                Console.Write("Drücke [Enter] zum Drehen oder gib 'q' zum Zurückkehren ein: ");
                var input = Console.ReadLine()?.Trim().ToLower();

                if (input == "q") return;
                if (!string.IsNullOrEmpty(input)) continue;

                konto -= spinCost;
                Console.WriteLine("\n🎲 Walzen drehen...");
                Thread.Sleep(300);

                string[] spin = {
                    symbols[rng.Next(symbols.Length)],
                    symbols[rng.Next(symbols.Length)],
                    symbols[rng.Next(symbols.Length)]
                };

                SpinAnimation(spin);

                if (spin[0] == spin[1] && spin[1] == spin[2])
                {
                    int win = GetGewinn(spin[0]);
                    konto += win;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n🎉 Drei {spin[0]}! Du gewinnst {win} Jetons!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n💀 Leider verloren.");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\n💸 Du hast zu wenig Jetons für weitere Spins.");
            Console.WriteLine("Drücke Enter, um zum Menü zurückzukehren.");
            Console.ReadLine();
        }

        private static int GetGewinn(string symbol) => symbol switch
        {
            "🍒" => 20,
            "💎" => 50,
            "🔔" => 75,
            "7️⃣" => 100,
            _ => 0
        };

        private static void SpinAnimation(string[] finalSymbols)
        {
            for (int i = 0; i < 12; i++)
            {
                var s1 = spinSymbols[rng.Next(spinSymbols.Length)];
                var s2 = spinSymbols[rng.Next(spinSymbols.Length)];
                var s3 = spinSymbols[rng.Next(spinSymbols.Length)];
                Console.Write($"\r| {s1} | {s2} | {s3} |");
                Thread.Sleep(60 + i * 10);
            }
            Console.Write($"\r| {finalSymbols[0]} | {finalSymbols[1]} | {finalSymbols[2]} |\n");
        }
    }

    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "🎲 AA's Casino Terminal";

            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("🎰 Willkommen im Online-Casino der doppel A's!");
                Console.ResetColor();
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"💰 Jetons: {SlotMachine.konto}");
                Console.WriteLine("1 - SlotMachine spielen");
                Console.WriteLine("2 - BlackJack spielen");
                Console.WriteLine("3 - Beenden");
                Console.Write("▶ Deine Auswahl: ");
                string choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        SlotMachine.Start();
                        break;
                    case "2":
                        StartBlackjack();
                        break;
                    case "3":
                        Console.WriteLine("👋 Danke fürs Spielen!");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("⛔ Ungültige Eingabe. Drücke Enter...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static void StartBlackjack()
        {
            var deck = new Deck();

            int numOpponents;
            while (true)
            {
                Console.Write("Wie viele Computergegner sollen mitspielen? (1-3): ");
                if (int.TryParse(Console.ReadLine(), out numOpponents) && numOpponents is >= 1 and <= 3)
                    break;
                Console.WriteLine("⛔ Ungültige Eingabe.");
            }

            var player = new Player("Du", false, SlotMachine.konto);
            var opponents = new List<Player>();
            for (int i = 1; i <= numOpponents; i++)
                opponents.Add(new Player($"Gegner {i}", true));

            bool playing = true;
            while (player.Money > 0 && playing)
            {
                Console.WriteLine($"\n💰 Dein Guthaben: {player.Money}. Setze deinen Einsatz:");
                if (!int.TryParse(Console.ReadLine(), out int bet) || bet <= 0 || bet > player.Money)
                {
                    Console.WriteLine("⛔ Ungültiger Einsatz.");
                    continue;
                }

                player.Bet = bet;
                player.Money -= bet;
                deck = new Deck();
                player.ClearHand();
                opponents.ForEach(o => o.ClearHand());

                player.AddCard(deck.DrawCard());
                player.AddCard(deck.DrawCard());
                opponents.ForEach(o =>
                {
                    o.AddCard(deck.DrawCard());
                    o.AddCard(deck.DrawCard());
                });

                player.ShowHand();
                opponents.ForEach(o => o.ShowHand());

                bool busted = false;
                while (!busted)
                {
                    Console.Write("Möchtest du 'hit' oder 'stand'? ");
                    string input = Console.ReadLine()?.ToLower();
                    if (input == "hit")
                    {
                        player.AddCard(deck.DrawCard());
                        player.ShowHand();
                        if (player.GetScore() > 21)
                        {
                            Console.WriteLine("💥 Überkauft!");
                            busted = true;
                        }
                    }
                    else if (input == "stand")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("⛔ Ungültige Eingabe.");
                    }
                }

                opponents.ForEach(o =>
                {
                    while (o.GetScore() < 17)
                        o.AddCard(deck.DrawCard());
                    o.ShowHand();
                });

                foreach (var opp in opponents)
                {
                    int playerScore = player.GetScore();
                    int dealerScore = opp.GetScore();

                    if (busted || (dealerScore <= 21 && dealerScore > playerScore))
                        Console.WriteLine($"{opp.Name} gewinnt.");
                    else if (playerScore == dealerScore)
                    {
                        Console.WriteLine($"Unentschieden mit {opp.Name}. Einsatz zurück.");
                        player.Money += bet;
                    }
                    else
                    {
                        Console.WriteLine($"Du schlägst {opp.Name}!");
                        player.Money += bet * 2;
                    }
                }

                Console.WriteLine($"🟢 Guthaben: {player.Money}");
                if (player.Money <= 0)
                {
                    Console.WriteLine("💸 Du bist pleite!");
                    break;
                }

                Console.Write("Noch eine Runde? (y/n): ");
                playing = Console.ReadLine()?.ToLower() == "y";
            }

            SlotMachine.konto = player.Money;
            Console.WriteLine("🎲 Spiel beendet. Drücke Enter...");
            Console.ReadLine();
        }
    }
}
