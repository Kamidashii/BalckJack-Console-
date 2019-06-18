using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSL_Layer.Models;
using HelpfulValues.Constants;
using HelpfulValues.Enums;



namespace BSL_Layer.Services
{
    public class BotService : BasicService
    {
        public BotService(List<User> players, List<Deck> decks, Croupier croupier) : base(players, decks, croupier)
        { }

        public void DesperateBotAction(Bot bot)
        {
            while (!IsPlayerWonScore(bot) && IsPlayerScoreValid(bot))
            {
                BotGetCard(bot, PullOutCard());
                RecalculateScore(bot);
            }
        }

        public void NormalBotAction(Bot bot)
        {
            while (bot.Score <= Bot_Constants.NORMAL_BOT_MAX_SCORE && IsPlayerScoreValid(bot))
            {
                BotGetCard(bot, PullOutCard());
                RecalculateScore(bot);
            }
        }

        public void SafeBotAction(Bot bot)
        {
            while (bot.Score <= Bot_Constants.SAFE_BOT_MAX_SCORE && IsPlayerScoreValid(bot))
            {
                BotGetCard(bot, PullOutCard());
                RecalculateScore(bot);
            }
        }

        public void BotGetCard(Bot bot, Card card)
        {
            bot.Cards.Add(card);
            bot.Score += card.GetCost();
        }

        public override Player MakePlayerClone(Player original)
        {
            Bot origin = original as Bot;
            Player copy = new Bot(origin.Name, origin.Bet, origin.Demeanor, origin.Score, origin.Cards);
            return copy;
        }
    }
}
