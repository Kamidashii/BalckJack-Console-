using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSL_Layer.Interfaces;
using BSL_Layer.Models;
using HelpfulValues.Constants;
using HelpfulValues.Enums;



namespace BSL_Layer.Services
{
    public class BotService : BasicService
    {
        public BotService(List<IPlayer> players, List<Deck> decks, IPlayer croupier) : base(players, decks, croupier)
        { }

        public void DesperateBotAction(IPlayer bot)
        {
            while (!IsPlayerWonScore(bot) && IsPlayerScoreValid(bot))
            {
                PlayerGetCard(bot, PullOutCard());
                RecalculateScore(bot);
            }
        }

        public void NormalBotAction(IPlayer bot)
        {
            while (bot.Score <= Bot_Constants.NORMAL_BOT_MAX_SCORE && IsPlayerScoreValid(bot))
            {
                PlayerGetCard(bot, PullOutCard());
                RecalculateScore(bot);
            }
        }

        public void SafeBotAction(IPlayer bot)
        {
            while (bot.Score <= Bot_Constants.SAFE_BOT_MAX_SCORE && IsPlayerScoreValid(bot))
            {
                PlayerGetCard(bot, PullOutCard());
                RecalculateScore(bot);
            }
        }

        public override IPlayer MakePlayerClone(IPlayer original)
        {
            Bot origin = original as Bot;
            IPlayer copy = new Bot(origin.Name, origin.Bet, origin.Demeanor, origin.Score, origin.Cards);
            return copy;
        }
    }
}
