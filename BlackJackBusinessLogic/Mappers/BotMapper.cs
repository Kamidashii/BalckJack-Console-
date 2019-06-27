using System;

namespace BlackJackBusinessLogic.Mappers
{
    public class BotMapper:PlayerMapper, Interfaces.IMapper<BlackJackBusinessLogic.Models.Bot,BlackJackDataAccess.Models.Bot>
    {
        public BlackJackBusinessLogic.Models.Bot ConvertItemToBusinessLogic(BlackJackDataAccess.Models.Bot DataAccessBot)
        {
            BlackJackBusinessLogic.Models.Bot BusinessLogicBot = new BlackJackBusinessLogic.Models.Bot(DataAccessBot.Name, DataAccessBot.Bet,DataAccessBot.Demeanor, DataAccessBot.Score, ConvertCardsToBusinessLogic(DataAccessBot.Cards),DataAccessBot.IsBot);

            return BusinessLogicBot;
        }

        public BlackJackDataAccess.Models.Bot ConvertItemToDataAccess(BlackJackBusinessLogic.Models.Bot BusinessLogicBot)
        {
            BlackJackDataAccess.Models.Bot DABot = new BlackJackDataAccess.Models.Bot(BusinessLogicBot.Name, BusinessLogicBot.Bet, BusinessLogicBot.Demeanor, BusinessLogicBot.Score, ConvertCardsToDataAccess(BusinessLogicBot.Cards), BusinessLogicBot.IsBot);

            return DABot;
        }
    }
}
