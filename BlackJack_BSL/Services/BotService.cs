using System;
using System.Collections.Generic;
using BlackJack_BSL.Interfaces.Models;
using BlackJack_BSL.Interfaces.Services;
using BlackJack_BSL.Models;
using Common.Constants;



namespace BlackJack_BSL.Services
{
    public class BotService : BasicService,IBotService
    {
        public BotService(List<Interfaces.Models.IUser> players, List<IDeck> decks, Interfaces.Models.IPlayer croupier) : base(players, decks, croupier)
        { }

        public void StartBotTurn(Interfaces.Models.IPlayer bot)
        {
            Interfaces.Models.IBot castedBot = bot as Interfaces.Models.IBot;

            switch (castedBot.Demeanor)
            {
                case Common.Enums.BotDemeanors.BotDemeanor.Desperate:
                    DesperateBotAction(castedBot);
                    break;
                case Common.Enums.BotDemeanors.BotDemeanor.Normal:
                    NormalBotAction(castedBot);
                    break;
                case Common.Enums.BotDemeanors.BotDemeanor.Safe:
                    SafeBotAction(castedBot);
                    break;
                default:
                    break;
            }
        }

        private void DesperateBotAction(Interfaces.Models.IPlayer bot)
        {
            PlayerGetCard(bot, PullOutCard());
            RecalculateScore(bot);

            if (!IsPlayerWonScore(bot) && IsPlayerScoreValid(bot))
                DesperateBotAction(bot);
        }

        private void NormalBotAction(Interfaces.Models.IPlayer bot)
        {
            PlayerGetCard(bot, PullOutCard());
            RecalculateScore(bot);

            if (bot.Score <= Bot_Constants.NormalBotMaxScore && IsPlayerScoreValid(bot))
                NormalBotAction(bot);
        }

        private void SafeBotAction(Interfaces.Models.IPlayer bot)
        {
            PlayerGetCard(bot, PullOutCard());
            RecalculateScore(bot);

            if (bot.Score <= Bot_Constants.SafeBotMaxScore && IsPlayerScoreValid(bot))
                SafeBotAction(bot);
        }

        public override Interfaces.Models.IPlayer MakePlayerClone(Interfaces.Models.IPlayer original)
        {
            Bot origin = original as Bot;
            Interfaces.Models.IUser copy = new Bot(origin.Name, origin.Bet, origin.Demeanor, origin.Score, origin.Cards, true);
            return copy;
        }
    }
}
