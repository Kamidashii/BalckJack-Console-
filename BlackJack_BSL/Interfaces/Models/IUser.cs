using System;

namespace BlackJack_BSL.Interfaces.Models
{
    public interface IUser:IPlayer
    {
        int Bet { get; set; }
        string Name { get; set; }
    }
}
