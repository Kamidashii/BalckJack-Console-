using System;

namespace BlackJack_BSL.Interfaces
{
    public interface IProfile
    {
        string Login { get; set; }
        string Password { get; set; }

        IUser User { get; set; }
    }
}
