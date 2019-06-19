using BSL_Layer.Interfaces;
using BSL_Layer.Models;
using HelpfulValues.Constants;
using HelpfulValues.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Views
{
    public class MainView
    {
        #region Players


        #region User

        public static void ShowUserTurn(User user)
        {
            Console.WriteLine($"\nUser {user.Name} get a turn");
        }

        public static void ShowUserActionInstruction()
        {
            Console.WriteLine("\n Choose action: \n 1 - Take a card \n 2 - Enough \n 3 - Surrender \n 4 - ShowCards");
        }

        public static User_Enums.ActionType FindOutUserAction()
        {
            User_Enums.ActionType choosedAction;
            Enum.TryParse(Console.ReadLine(), out choosedAction);
            return choosedAction;
        }

        public static void ShowUserWon(User user)
        {
            Console.WriteLine($"\n User {user.Name} has won {user.Bet * GameService_Constants.BET_RATIO}!");
        }

        public static void ShowUserLost(User user)
        {
            Console.WriteLine($"\n User {user.Name} has lost {user.Bet}");
        }

        public static void ShowUserDraw(User user)
        {
            Console.WriteLine($"\n User {user.Name} has got a draw!");
        }

        public static void ShowUserCardGetting(User user)
        {
            Console.WriteLine($"User {user.Name} taked a card");
        }

        public static void ShowUserSpecificCardGetting(User user)
        {
            Console.WriteLine($"{user.Name} taked {GetCardInfo(user.Cards.Last())})");
        }

        public static void ShowBotSpecificCardGetting(Bot bot)
        {
            Console.WriteLine($"{bot.Name} taked {GetCardInfo(bot.Cards.Last())})");
        }

        public static void ShowUserScore(User user)
        {
            Console.WriteLine($"\nUser {user.Name}'s score now is: {user.Score}\n");
        }

        #endregion

        #region Croupier

        public static void ShowCroupierScore(IPlayer croupier)
        {
            Console.WriteLine($"Croupier's score now is: {croupier.Score}");
        }

        public static void ShowCroupierGetTurn()
        {
            Console.WriteLine("Croupier get a turn");
        }

        public static void ShowCroupierCardGetting()
        {
            Console.WriteLine("Croupier taked a card");
        }

        #endregion

        #region Bot

        public static void ShowBotTurn(Bot bot)
        {
            Console.WriteLine($"\nBot {bot.Name} get a turn");
        }

        public static void ShowBotCardGetting(Bot bot)
        {
            Console.WriteLine($"Bot {bot.Name} taked a card");
        }

        #endregion

        public static void ShowOverfeedScoreMessage(User user)
        {
            Console.WriteLine($"User {user.Name} score was overfeeded!");
        }

        public static void ShowGreatScoreMessage(User user)
        {
            Console.WriteLine($"{user.Name} have a great score!");
        }

        #endregion

        public static void ShowInvalidChoose()
        {
            Console.WriteLine("Invalid choose!");
        }

        public static void ShowCard(ICard card)
        {
            Console.WriteLine(GetCardInfo(card));
        }

        public static void ShowGameId(int id)
        {
            Console.Clear();
            Console.WriteLine($"Started game number: {id + 1}\n");
        }

        public static void ShowWaitNextGame()
        {
            Console.WriteLine("\n Waiting for a new game. Are you ready? (Press anything if you ready)");
            Console.ReadLine();
        }

        public static void ShowGamesOverMessage()
        {
            Console.WriteLine("\n\nAll games over. Results saved.");
        }

        public static void ShowExceptionMessage(string message)
        {
            Console.WriteLine("\nException: " + message);
        }

        public static string GetCardInfo(ICard card)
        {
            return $"{Enum.GetName(typeof(Card_Enums.CardRank), card.Rank)} {Enum.GetName(typeof(Card_Enums.CardSuit), card.Suit)} (Cost: {card.Cost})";
        }

        public static void ShowResults(List<GameResult> gameResults)
        {
            Console.Clear();

            Console.WriteLine($"\t All games: {gameResults.Count}\n\n");
            for (int i = 0; i < gameResults.Count; ++i)
            {
                Console.WriteLine($"\t Game number: {i + 1} \n");
                Console.WriteLine("<Winners: ");
                ShowPlayers(gameResults[i].Winners);
                Console.WriteLine("Winners>\n");

                Console.WriteLine("<Losers: ");
                ShowPlayers(gameResults[i].Losers);
                Console.WriteLine("Losers>\n");

                Console.WriteLine("<Draw: ");
                ShowPlayers(gameResults[i].Draws);
                Console.WriteLine("Draw>\n");

                Console.WriteLine($"Croupier score: {gameResults[i].Croupier.Score}\n\n");
            }
        }

        public static void AskResults(List<GameResult> gameResults)
        {
            Console.WriteLine("Show games results? (If yes press 'y')");
            string res = Console.ReadLine();
            res = res.ToLower();
            if (res.CompareTo("y") == 0)
            {
                ShowResults(gameResults);
            }
        }

        public static void ShowPlayers(List<User> players)
        {
            StringBuilder builder = new StringBuilder();


            for (int i = 0; i < players.Count; ++i)
            {
                User player = players[i];

                if (players[i].IsBot)
                    builder.Append("_Bot_ ");
                else
                    builder.Append("_Player_ ");

                builder.Append(player.Name).Append(" Score: " + player.Score).Append(" Bet: " + player.Bet).Append("\n");

                Console.WriteLine(builder.ToString());
                builder.Clear();
            }
        }

        public static void GetLoginAndPassword(out string login, out string password)
        {
            Console.WriteLine("You need to authorize");

            Console.WriteLine("Enter your login please");
            login = Console.ReadLine();

            Console.WriteLine("Enter your password please");
            password = Console.ReadLine();

        }

        public static void IncorrectLoginOrPassword()
        {
            Console.Clear();
            Console.WriteLine("Incorrect login or password");
        }

        public static void WelcomeUser(string name)
        {
            Console.WriteLine("Welcome, " + name + "!");
        }
    }
}
