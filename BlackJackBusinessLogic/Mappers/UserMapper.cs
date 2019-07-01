using System;

namespace BlackJackBusinessLogic.Mappers
{
    public class UserMapper:PlayerMapper, Interfaces.IMapper<BlackJackBusinessLogic.Interfaces.Models.IUser,BlackJackDataAccess.Models.User>
    {
        public BlackJackBusinessLogic.Interfaces.Models.IUser ConvertItemToBusinessLogic(BlackJackDataAccess.Models.User DataAccessUser)
        {
            var cards = ConvertCardsToBusinessLogic(DataAccessUser.Cards);

            var BusinessLogicUser = new Models.User(DataAccessUser.Name, DataAccessUser.Bet, DataAccessUser.Score, cards, DataAccessUser.IsBot);

            return BusinessLogicUser;
        }

        public BlackJackDataAccess.Models.User ConvertItemToDataAccess(BlackJackBusinessLogic.Interfaces.Models.IUser BusinessLogicUser)
        {
            var cards = ConvertCardsToDataAccess(BusinessLogicUser.Cards);

            var DataAccessUser = new BlackJackDataAccess.Models.User(BusinessLogicUser.Name, BusinessLogicUser.Bet, BusinessLogicUser.Score, cards, BusinessLogicUser.IsBot);

            return DataAccessUser;
        }
    }
}
