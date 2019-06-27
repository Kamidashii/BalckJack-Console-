using System;

namespace BlackJackBusinessLogic.Mappers
{
    public class UserMapper:PlayerMapper, Interfaces.IMapper<BlackJackBusinessLogic.Interfaces.Models.IUser,BlackJackDataAccess.Models.User>
    {
        public BlackJackBusinessLogic.Interfaces.Models.IUser ConvertItemToBusinessLogic(BlackJackDataAccess.Models.User DataAccessUser)
        {
            BlackJackBusinessLogic.Models.User BusinessLogicUser = new Models.User(DataAccessUser.Name, DataAccessUser.Bet, DataAccessUser.Score, ConvertCardsToBusinessLogic(DataAccessUser.Cards),DataAccessUser.IsBot);

            return BusinessLogicUser;
        }

        public BlackJackDataAccess.Models.User ConvertItemToDataAccess(BlackJackBusinessLogic.Interfaces.Models.IUser BusinessLogicUser)
        {
            BlackJackDataAccess.Models.User DataAccessUser = new BlackJackDataAccess.Models.User(BusinessLogicUser.Name, BusinessLogicUser.Bet, BusinessLogicUser.Score, ConvertCardsToDataAccess(BusinessLogicUser.Cards),BusinessLogicUser.IsBot);

            return DataAccessUser;
        }
    }
}
