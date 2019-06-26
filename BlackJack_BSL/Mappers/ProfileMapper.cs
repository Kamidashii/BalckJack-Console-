using System;

namespace BlackJack_BSL.Mappers
{
    public class ProfileMapper: Interfaces.IMapper<BlackJack_BSL.Interfaces.Models.IProfile,BlackJack_DA.Models.Profile>
    {
        private UserMapper _userMapper;

        public ProfileMapper()
        {
            this._userMapper = new UserMapper();
        }

        public BlackJack_BSL.Interfaces.Models.IProfile ConvertItemToBusinessLogic(BlackJack_DA.Models.Profile DataAccessProfile)
        {
            BlackJack_BSL.Models.Profile BusinessLogicProfile = new BlackJack_BSL.Models.Profile(DataAccessProfile.Login, DataAccessProfile.Password, _userMapper.ConvertItemToBusinessLogic(DataAccessProfile.User));

            return BusinessLogicProfile;
        }

        public BlackJack_DA.Models.Profile ConvertItemToDataAccess(BlackJack_BSL.Interfaces.Models.IProfile BusinessLogicProfile)
        {
            BlackJack_DA.Models.Profile DataAccessProfile = new BlackJack_DA.Models.Profile(BusinessLogicProfile.Login, BusinessLogicProfile.Password);

            return DataAccessProfile;
        }
    }
}
