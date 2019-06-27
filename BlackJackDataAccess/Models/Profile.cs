using System;

namespace BlackJackDataAccess.Models
{
    public class Profile
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public User User { get; set; }

        public Profile(string login,string password)
        {
            this.Login = login;
            this.Password = password;
        }

        public Profile(string login, string password,User user)
        {
            this.Login = login;
            this.Password = password;
            this.User = user;
        }
    }
}
