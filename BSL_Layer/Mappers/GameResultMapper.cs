using System;
using System.Collections.Generic;

namespace BlackJack_BSL.Mappers
{
    public class GameResultMapper: Interfaces.IMapper<BlackJack_BSL.Models.GameResult,BlackJack_DA.Models.GameResult>
    {
        private UserMapper _userMapper;
        private CroupierMapper _croupierMapper;

        public GameResultMapper()
        {
            _userMapper = new UserMapper();
            _croupierMapper = new CroupierMapper();
        }

        public BlackJack_BSL.Models.GameResult ConvertItemToBusinessLogic(BlackJack_DA.Models.GameResult DataAccessresult)
        {
            BlackJack_BSL.Models.GameResult BusinessLogicResult = new Models.GameResult(
                DataAccessresult.GameId,
                DataAccessresult.AllGamesCount,
                ConvertUsersListToBSL(DataAccessresult.Winners),
                ConvertUsersListToBSL(DataAccessresult.Losers),
                ConvertUsersListToBSL(DataAccessresult.Draws),
                _croupierMapper.ConvertItemToBusinessLogic(DataAccessresult.Croupier));

            return BusinessLogicResult;
        }

        public BlackJack_DA.Models.GameResult ConvertItemToDataAccess(BlackJack_BSL.Models.GameResult BusinessLogicResult)
        {
            BlackJack_DA.Models.GameResult DataAccessResult = new BlackJack_DA.Models.GameResult(BusinessLogicResult.GameId,
               BusinessLogicResult.AllGamesCount,
               ConvertUsersToDA(BusinessLogicResult.Winners),
               ConvertUsersToDA(BusinessLogicResult.Losers),
               ConvertUsersToDA(BusinessLogicResult.Draws),
               _croupierMapper.ConvertItemToDataAccess(BusinessLogicResult.Croupier));

            return DataAccessResult;
        }

        private List<BlackJack_BSL.Interfaces.Models.IUser> ConvertUsersListToBSL(List<BlackJack_DA.Models.User> DataAccessUsersList)
        {
            List<BlackJack_BSL.Interfaces.Models.IUser> BusinessLogicUsersList = new List<BlackJack_BSL.Interfaces.Models.IUser>();

            for (int i = 0; i < DataAccessUsersList.Count; ++i)
            {
                BusinessLogicUsersList.Add(_userMapper.ConvertItemToBusinessLogic(DataAccessUsersList[i]));
            }

            return BusinessLogicUsersList;
        }
        private List<BlackJack_DA.Models.User> ConvertUsersToDA(List<BlackJack_BSL.Interfaces.Models.IUser> BusinessLogicUsersList)
        {
            List<BlackJack_DA.Models.User> DataAccessUsersList = new List<BlackJack_DA.Models.User>();

            for (int i = 0; i < BusinessLogicUsersList.Count; ++i)
            {
                DataAccessUsersList.Add(_userMapper.ConvertItemToDataAccess(BusinessLogicUsersList[i]));
            }

            return DataAccessUsersList;
        }
    }
}
