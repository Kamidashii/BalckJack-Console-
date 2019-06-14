using BlackJack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Views;

namespace BlackJack.Managers
{
    public class BotManager : BasicManager
    {
        public BotManager(List<User> players, List<Deck> decks, Croupier croupier) : base(players, decks, croupier)
        { }

        public void DesperateBotAction(Bot bot)
        {
            while (!IsPlayerWonScore(bot) && IsPlayerScoreValid(bot))
            {
                BotGetCard(bot, PullOutCard());
                MainView.ShowBotSpecificCardGetting(bot);
                RecalculateScore(bot);
            }
        }

        public void NormalBotAction(Bot bot)
        {
            while (bot.Score <= 15 && IsPlayerScoreValid(bot))
            {
                BotGetCard(bot, PullOutCard());
                MainView.ShowBotSpecificCardGetting(bot);
                RecalculateScore(bot);
            }
        }

        public void SafeBotAction(Bot bot)
        {
            while (bot.Score <= 11 && IsPlayerScoreValid(bot))
            {
                BotGetCard(bot, PullOutCard());
                MainView.ShowBotSpecificCardGetting(bot);
                RecalculateScore(bot);
            }
        }

        public void StartBotTurn(Bot bot)
        {
            MainView.ShowBotTurn(bot);

            switch (bot.Demeanor)
            {
                case Enums.Bot_Enums.Bot_Demeanor.Desperate:
                    DesperateBotAction(bot);
                    break;
                case Enums.Bot_Enums.Bot_Demeanor.Normal:
                    NormalBotAction(bot);
                    break;
                case Enums.Bot_Enums.Bot_Demeanor.Safe:
                    SafeBotAction(bot);
                    break;
                default:
                    break;
            }
        }

        public void BotGetCard(Bot bot, Card card)
        {
            bot.Cards.Add(card);
            bot.Score += card.GetCost();
            MainView.ShowBotCardGetting(bot);
        }

        public override Player MakePlayerClone(Player original)
        {
            Bot origin = original as Bot;
            Player copy = new Bot(origin.Name, origin.Bet, origin.Demeanor, origin.Score, origin.Cards);
            return copy;
        }
    }
}
