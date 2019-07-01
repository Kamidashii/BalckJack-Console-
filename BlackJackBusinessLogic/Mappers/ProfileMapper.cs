using System;

namespace BlackJackBusinessLogic.Mappers
{
    public class ProfileMapper: Interfaces.IMapper<BlackJackBusinessLogic.Interfaces.Models.IProfile,BlackJackDataAccess.Models.Profile>
    {
        private UserMapper _userMapper;

        public ProfileMapper()
        {
            _userMapper = new UserMapper();
        }

        public BlackJackBusinessLogic.Interfaces.Models.IProfile ConvertItemToBusinessLogic(BlackJackDataAccess.Models.Profile DataAccessProfile)
        {
            var BusinessLogicProfile = new BlackJackBusinessLogic.Models.Profile(DataAccessProfile.Login, DataAccessProfile.Password, _userMapper.ConvertItemToBusinessLogic(DataAccessProfile.User));

            return BusinessLogicProfile;
        }

        public BlackJackDataAccess.Models.Profile ConvertItemToDataAccess(BlackJackBusinessLogic.Interfaces.Models.IProfile BusinessLogicProfile)
        {
            var DataAccessProfile = new BlackJackDataAccess.Models.Profile(BusinessLogicProfile.Login, BusinessLogicProfile.Password);

            return DataAccessProfile;
        }
    }
}
