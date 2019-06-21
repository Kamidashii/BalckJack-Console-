using System;

namespace BlackJack_BSL.Mappers
{
    public class ProfileMapper: Interfaces.IMapper<BlackJack_BSL.Interfaces.IProfile,BlackJack_DA.Models.Profile>
    {
        private UserMapper userMapper;

        public ProfileMapper()
        {
            this.userMapper = new UserMapper();
        }

        public BlackJack_BSL.Interfaces.IProfile ConvertItemToBSL(BlackJack_DA.Models.Profile DAProfile)
        {
            BlackJack_BSL.Models.Profile BSLProfile = new BlackJack_BSL.Models.Profile(DAProfile.Login, DAProfile.Password, userMapper.ConvertItemToBSL(DAProfile.User));

            return BSLProfile;
        }

        public BlackJack_DA.Models.Profile ConvertItemToDA(BlackJack_BSL.Interfaces.IProfile BSLProfile)
        {
            BlackJack_DA.Models.Profile DAProfile = new BlackJack_DA.Models.Profile(BSLProfile.Login, BSLProfile.Password);

            return DAProfile;
        }
    }
}
