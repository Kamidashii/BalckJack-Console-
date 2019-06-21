using System;

namespace BlackJack_BSL.Mappers
{
    public class UserMapper:PlayerMapper, Interfaces.IMapper<BlackJack_BSL.Interfaces.Models.IUser,BlackJack_DA.Models.User>
    {
        public BlackJack_BSL.Interfaces.Models.IUser ConvertItemToBusinessLogic(BlackJack_DA.Models.User DataAccessUser)
        {
            BlackJack_BSL.Models.User BusinessLogicUser = new Models.User(DataAccessUser.Name, DataAccessUser.Bet, DataAccessUser.Score, ConvertCardsToBusinessLogic(DataAccessUser.Cards),DataAccessUser.IsBot);

            return BusinessLogicUser;
        }

        public BlackJack_DA.Models.User ConvertItemToDataAccess(BlackJack_BSL.Interfaces.Models.IUser BusinessLogicUser)
        {
            BlackJack_DA.Models.User DataAccessUser = new BlackJack_DA.Models.User(BusinessLogicUser.Name, BusinessLogicUser.Bet, BusinessLogicUser.Score, ConvertCardsToDataAccess(BusinessLogicUser.Cards),BusinessLogicUser.IsBot);

            return DataAccessUser;
        }
    }
}
