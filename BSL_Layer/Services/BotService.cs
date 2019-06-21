using System;
using System.Collections.Generic;
using BlackJack_BSL.Interfaces;
using BlackJack_BSL.Models;
using Common.Constants;



namespace BlackJack_BSL.Services
{
    public class BotService : BasicService
    {
        public BotService(List<IUser> players, List<Deck> decks, IPlayer croupier) : base(players, decks, croupier)
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
            while (bot.Score <= Bot_Constants.NormalBotMaxScore && IsPlayerScoreValid(bot))
            {
                PlayerGetCard(bot, PullOutCard());
                RecalculateScore(bot);
            }
        }

        public void SafeBotAction(IPlayer bot)
        {
            while (bot.Score <= Bot_Constants.SafeBotMaxScore && IsPlayerScoreValid(bot))
            {
                PlayerGetCard(bot, PullOutCard());
                RecalculateScore(bot);
            }
        }

        public override IPlayer MakePlayerClone(IPlayer original)
        {
            Bot origin = original as Bot;
            IUser copy = new Bot(origin.Name, origin.Bet, origin.Demeanor, origin.Score, origin.Cards,true);
            return copy;
        }
    }
}
