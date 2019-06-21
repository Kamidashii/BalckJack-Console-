using System;

namespace BlackJack_BSL.Mappers
{
    public class BotMapper:PlayerMapper, Interfaces.IMapper<BlackJack_BSL.Models.Bot,BlackJack_DA.Models.Bot>
    {
        public BlackJack_BSL.Models.Bot ConvertItemToBusinessLogic(BlackJack_DA.Models.Bot DataAccessBot)
        {
            BlackJack_BSL.Models.Bot BusinessLogicBot = new BlackJack_BSL.Models.Bot(DataAccessBot.Name, DataAccessBot.Bet,DataAccessBot.Demeanor, DataAccessBot.Score, ConvertCardsToBusinessLogic(DataAccessBot.Cards),DataAccessBot.IsBot);

            return BusinessLogicBot;
        }

        public BlackJack_DA.Models.Bot ConvertItemToDataAccess(BlackJack_BSL.Models.Bot BSLBot)
        {
            BlackJack_DA.Models.Bot DABot = new BlackJack_DA.Models.Bot(BSLBot.Name, BSLBot.Bet, BSLBot.Demeanor, BSLBot.Score, ConvertCardsToDataAccess(BSLBot.Cards), BSLBot.IsBot);

            return DABot;
        }
    }
}
