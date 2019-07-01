using System;
using System.Collections.Generic;
using BlackJackBusinessLogic.Interfaces.Models;
using BlackJackBusinessLogic.Interfaces.Services;
using BlackJackBusinessLogic.Models;
using Common.Constants;



namespace BlackJackBusinessLogic.Services
{
    public class BotService : BaseService,IBotService
    {
        private IDeckService _deckService;

        public BotService()
        {
            _deckService = new DeckService();
        }

        public void StartBotTurn(Interfaces.Models.IPlayer bot,List<IDeck>decks)
        {
            var castedBot = bot as Interfaces.Models.IBot;

            switch (castedBot.Demeanor)
            {
                case Common.Enums.BotDemeanors.BotDemeanor.Desperate:
                    {
                        DesperateBotAction(castedBot, decks);
                    }
                    break;
                case Common.Enums.BotDemeanors.BotDemeanor.Normal:
                    {
                        NormalBotAction(castedBot,decks);
                    }
                    break;
                case Common.Enums.BotDemeanors.BotDemeanor.Safe:
                    {
                        SafeBotAction(castedBot,decks);
                    }
                    break;
                default:
                    break;
            }
        }

        private void DesperateBotAction(Interfaces.Models.IPlayer bot,List<IDeck>decks)
        {
            ICard pullOutedCard = _deckService.PullOutCard(decks);
            PlayerGetCard(bot, pullOutedCard);
            RecalculateScore(bot);

            if (!IsPlayerWonScore(bot) && IsPlayerScoreValid(bot))
            {
                DesperateBotAction(bot,decks);
            }
        }

        private void NormalBotAction(Interfaces.Models.IPlayer bot,List<IDeck>decks)
        {
            ICard pullOutedCard = _deckService.PullOutCard(decks);
            PlayerGetCard(bot, pullOutedCard);
            RecalculateScore(bot);

            if (bot.Score <= Bot_Constants.NormalBotMaxScore && IsPlayerScoreValid(bot))
            {
                NormalBotAction(bot,decks);
            }
        }

        private void SafeBotAction(Interfaces.Models.IPlayer bot, List<IDeck> decks)
        {
            ICard pullOutedCard = _deckService.PullOutCard(decks);
            PlayerGetCard(bot, pullOutedCard);
            RecalculateScore(bot);

            if (bot.Score <= Bot_Constants.SafeBotMaxScore && IsPlayerScoreValid(bot))
            {
                SafeBotAction(bot,decks);
            }
        }

        public override Interfaces.Models.IPlayer MakePlayerClone(Interfaces.Models.IPlayer original)
        {
            var origin = original as Bot;
            var copy = new Bot(origin.Name, origin.Bet, origin.Demeanor, origin.Score, origin.Cards, true);

            return copy;
        }
    }
}
