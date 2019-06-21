using System;
using BlackJack_BSL.Interfaces;

namespace BlackJack_BSL.Models
{
    public class Profile : IProfile
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public IUser User { get; set; }

        public Profile(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }

        public Profile(string login, string password, IUser user)
        {
            this.Login = login;
            this.Password = password;

            this.User = user;
        }
    }
}
