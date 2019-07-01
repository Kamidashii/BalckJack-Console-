using System;

namespace BlackJackBusinessLogic.Mappers
{
    public class BotMapper:PlayerMapper, Interfaces.IMapper<BlackJackBusinessLogic.Models.Bot,BlackJackDataAccess.Models.Bot>
    {
        public BlackJackBusinessLogic.Models.Bot ConvertItemToBusinessLogic(BlackJackDataAccess.Models.Bot DataAccessBot)
        {
            var cards = ConvertCardsToBusinessLogic(DataAccessBot.Cards);

            var BusinessLogicBot = new BlackJackBusinessLogic.Models.Bot(DataAccessBot.Name, DataAccessBot.Bet,DataAccessBot.Demeanor, DataAccessBot.Score, cards, DataAccessBot.IsBot);

            return BusinessLogicBot;
        }

        public BlackJackDataAccess.Models.Bot ConvertItemToDataAccess(BlackJackBusinessLogic.Models.Bot BusinessLogicBot)
        {
            var cards = ConvertCardsToDataAccess(BusinessLogicBot.Cards);

            var DataAccessBot = new BlackJackDataAccess.Models.Bot(BusinessLogicBot.Name, BusinessLogicBot.Bet, BusinessLogicBot.Demeanor, BusinessLogicBot.Score, cards, BusinessLogicBot.IsBot);

            return DataAccessBot;
        }
    }
}
