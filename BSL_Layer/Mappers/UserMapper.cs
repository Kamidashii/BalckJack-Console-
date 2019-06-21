using System;

namespace BlackJack_BSL.Mappers
{
    public class UserMapper:PlayerMapper, Interfaces.IMapper<BlackJack_BSL.Interfaces.IUser,BlackJack_DA.Models.User>
    {
        public BlackJack_BSL.Interfaces.IUser ConvertItemToBSL(BlackJack_DA.Models.User DAUser)
        {
            BlackJack_BSL.Models.User BSLUser = new Models.User(DAUser.Name, DAUser.Bet, DAUser.Score, ConvertCardsToBSL(DAUser.Cards),DAUser.IsBot);

            return BSLUser;
        }

        public BlackJack_DA.Models.User ConvertItemToDA(BlackJack_BSL.Interfaces.IUser BSLUser)
        {
            BlackJack_DA.Models.User DAUser = new BlackJack_DA.Models.User(BSLUser.Name, BSLUser.Bet, BSLUser.Score, ConvertCardsToDA(BSLUser.Cards),BSLUser.IsBot);

            return DAUser;
        }
    }
}
