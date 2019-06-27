using System;
using System.Collections.Generic;

namespace BlackJackBusinessLogic.Mappers
{
    public class GameResultMapper: Interfaces.IMapper<BlackJackBusinessLogic.Models.GameResult,BlackJackDataAccess.Models.GameResult>
    {
        private UserMapper _userMapper;
        private CroupierMapper _croupierMapper;

        public GameResultMapper()
        {
            _userMapper = new UserMapper();
            _croupierMapper = new CroupierMapper();
        }

        public BlackJackBusinessLogic.Models.GameResult ConvertItemToBusinessLogic(BlackJackDataAccess.Models.GameResult DataAccessResult)
        {
            BlackJackBusinessLogic.Models.GameResult BusinessLogicResult = new Models.GameResult(
                DataAccessResult.GameId,
                DataAccessResult.AllGamesCount,
                ConvertUsersListToBSL(DataAccessResult.Winners),
                ConvertUsersListToBSL(DataAccessResult.Losers),
                ConvertUsersListToBSL(DataAccessResult.Draws),
                _croupierMapper.ConvertItemToBusinessLogic(DataAccessResult.Croupier));

            return BusinessLogicResult;
        }

        public BlackJackDataAccess.Models.GameResult ConvertItemToDataAccess(BlackJackBusinessLogic.Models.GameResult BusinessLogicResult)
        {
            BlackJackDataAccess.Models.GameResult DataAccessResult = new BlackJackDataAccess.Models.GameResult(BusinessLogicResult.GameId,
               BusinessLogicResult.AllGamesCount,
               ConvertUsersToDA(BusinessLogicResult.Winners),
               ConvertUsersToDA(BusinessLogicResult.Losers),
               ConvertUsersToDA(BusinessLogicResult.Draws),
               _croupierMapper.ConvertItemToDataAccess(BusinessLogicResult.Croupier));

            return DataAccessResult;
        }

        private List<BlackJackBusinessLogic.Interfaces.Models.IUser> ConvertUsersListToBSL(List<BlackJackDataAccess.Models.User> DataAccessUsersList)
        {
            var BusinessLogicUsersList = new List<BlackJackBusinessLogic.Interfaces.Models.IUser>();

            for (int i = 0; i < DataAccessUsersList.Count; ++i)
            {
                BusinessLogicUsersList.Add(_userMapper.ConvertItemToBusinessLogic(DataAccessUsersList[i]));
            }

            return BusinessLogicUsersList;
        }
        private List<BlackJackDataAccess.Models.User> ConvertUsersToDA(List<BlackJackBusinessLogic.Interfaces.Models.IUser> BusinessLogicUsersList)
        {
            var DataAccessUsersList = new List<BlackJackDataAccess.Models.User>();

            for (int i = 0; i < BusinessLogicUsersList.Count; ++i)
            {
                DataAccessUsersList.Add(_userMapper.ConvertItemToDataAccess(BusinessLogicUsersList[i]));
            }

            return DataAccessUsersList;
        }
    }
}
