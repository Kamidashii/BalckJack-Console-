using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Constants;
using BlackJack;
using BlackJack.Enums;
using BlackJack.Models;

namespace Views
{
    public class MainView
    {
        #region User

        public void ShowUserTurn(User user)
        {
            Console.WriteLine($"\nUser {user.Name} get a turn");
        }

        public void ShowUserActionInstruction()
        {
            Console.WriteLine("\n Choose action: \n 1 - Take a card \n 2 - Enough \n 3 - Surrender \n 4 - ShowCards");
        }

        public void ShowUserWon(User user)
        {
            Console.WriteLine($"\n User {user.Name} has won {user.Bet * GameController_Constants.BET_RATIO}!");
        }

        public void ShowUserLost(User user)
        {
            Console.WriteLine($"\n User {user.Name} has lost {user.Bet}");
        }

        public void ShowUserDraw(User user)
        {
            Console.WriteLine($"\n User {user.Name} has got a draw!");
        }

        public void ShowUserCardGetting(User user)
        {
            Console.WriteLine($"User {user.Name} taked a card");
        }

        public void ShowUserSpecificCardGetting(User user)
        {
            Console.WriteLine($"{user.Name} taked {GetCardInfo(user.Cards.Last())})");
        }

        public void ShowBotSpecificCardGetting(Bot bot)
        {
            Console.WriteLine($"{bot.Name} taked {GetCardInfo(bot.Cards.Last())})");
        }

        public void ShowUserScore(User user)
        {
            Console.WriteLine($"\nUser {user.Name}'s score now is: {user.Score}\n");
        }

        #endregion

        #region Croupier

        public void ShowCroupierScore(Croupier croupier)
        {
            Console.WriteLine($"Croupier's score now is: {croupier.Score}");
        }

        public void ShowCroupierCardGetting()
        {
            Console.WriteLine("Croupier taked a card");
        }

        #endregion

        #region Bot

        public void ShowBotTurn(Bot bot)
        {
            Console.WriteLine($"\nBot {bot.Name} get a turn");
        }

        public void ShowBotCardGetting(Bot bot)
        {
            Console.WriteLine($"Bot {bot.Name} taked a card");
        }

        #endregion

        public void ShowInvalidChoose()
        {
            Console.WriteLine("Invalid choose!");
        }

        public void ShowCard(Card card)
        {
            Console.WriteLine(GetCardInfo(card));
        }

        public void ShowGameId(int id)
        {
            Console.Clear();
            Console.WriteLine($"Started game number: {id + 1}\n");
        }

        public void ShowWaitNextGame()
        {
            Console.WriteLine("Waiting for a new game. Are you ready? (Press anything if you ready)");
            Console.ReadLine();
        }

        public void ShowOverfeedScoreMessage(User user)
        {
            Console.WriteLine($"User {user.Name} score was overfeeded!");
        }

        public void ShowGreatScoreMessage(User user)
        {
            Console.WriteLine($"{user.Name} have a great score!");
        }

        public void ShowGamesOverMessage()
        {
            Console.WriteLine("\n\nAll games over. Results saved.");
        }

        public void ShowExceptionMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string GetCardInfo(Card card)
        {
            return $"{Enum.GetName(typeof(Card_Enums.CardRank), card.Rank)} {Enum.GetName(typeof(Card_Enums.CardSuit), card.Suit)} (Cost: {card.GetCost()})";
        }
    }
}
