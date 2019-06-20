using System;
using BSL_Layer.Interfaces;

namespace BSL_Layer.Models
{
    public class Profile : IProfile
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public IPlayer Player { get; set; }

        public Profile(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }

        public Profile(string login, string password, IPlayer player)
        {
            this.Login = login;
            this.Password = password;

            this.Player = player;
        }

        public IProfile GetProfileFromDB(DA_Layer.Models.Profile DAprofile)
        {
            Profile profile = new Profile(DAprofile.Login, DAprofile.Password, Player = new User(DAprofile.User));
            return profile;
        }

        public DA_Layer.Models.Profile GetProfileToDB()
        {
            DA_Layer.Models.Profile DAprofile = new DA_Layer.Models.Profile(this.Login, this.Password);

            return DAprofile;
        }
    }
}
