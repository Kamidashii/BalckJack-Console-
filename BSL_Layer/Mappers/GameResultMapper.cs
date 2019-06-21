using System;
using System.Collections.Generic;

namespace BlackJack_BSL.Mappers
{
    public class GameResultMapper: Interfaces.IMapper<BlackJack_BSL.Models.GameResult,BlackJack_DA.Models.GameResult>
    {
        private UserMapper userMapper;
        private CroupierMapper croupierMapper;

        public GameResultMapper()
        {
            userMapper = new UserMapper();
            croupierMapper = new CroupierMapper();
        }

        public BlackJack_BSL.Models.GameResult ConvertItemToBSL(BlackJack_DA.Models.GameResult DAresult)
        {
            BlackJack_BSL.Models.GameResult BSLResult = new Models.GameResult(
                DAresult.GameId,
                DAresult.AllGamesCount,
                ConvertUsersListToBSL(DAresult.Winners),
                ConvertUsersListToBSL(DAresult.Losers),
                ConvertUsersListToBSL(DAresult.Draws),
                croupierMapper.ConvertItemToBSL(DAresult.Croupier));

            return BSLResult;
        }

        public BlackJack_DA.Models.GameResult ConvertItemToDA(BlackJack_BSL.Models.GameResult BSLResult)
        {
            BlackJack_DA.Models.GameResult DAResult = new BlackJack_DA.Models.GameResult(BSLResult.GameId,
               BSLResult.AllGamesCount,
               ConvertUsersToDA(BSLResult.Winners),
               ConvertUsersToDA(BSLResult.Losers),
               ConvertUsersToDA(BSLResult.Draws),
               croupierMapper.ConvertItemToDA(BSLResult.Croupier));

            return DAResult;
        }

        private List<BlackJack_BSL.Interfaces.IUser> ConvertUsersListToBSL(List<BlackJack_DA.Models.User> DAUsersList)
        {
            List<BlackJack_BSL.Interfaces.IUser> BSLUsersList = new List<BlackJack_BSL.Interfaces.IUser>();

            for (int i = 0; i < DAUsersList.Count; ++i)
            {
                BSLUsersList.Add(userMapper.ConvertItemToBSL(DAUsersList[i]));
            }

            return BSLUsersList;
        }
        private List<BlackJack_DA.Models.User> ConvertUsersToDA(List<BlackJack_BSL.Interfaces.IUser> BSLUsersList)
        {
            List<BlackJack_DA.Models.User> DAUsersList = new List<BlackJack_DA.Models.User>();

            for (int i = 0; i < BSLUsersList.Count; ++i)
            {
                DAUsersList.Add(userMapper.ConvertItemToDA(BSLUsersList[i]));
            }

            return DAUsersList;
        }
    }
}
