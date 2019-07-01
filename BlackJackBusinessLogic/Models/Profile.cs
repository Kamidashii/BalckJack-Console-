﻿using System;
using BlackJackBusinessLogic.Interfaces;

namespace BlackJackBusinessLogic.Models
{
    public class Profile : Interfaces.Models.IProfile
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public Interfaces.Models.IUser User { get; set; }

        public Profile(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public Profile(string login, string password, Interfaces.Models.IUser user)
        {
            Login = login;
            Password = password;

            User = user;
        }
    }
}
