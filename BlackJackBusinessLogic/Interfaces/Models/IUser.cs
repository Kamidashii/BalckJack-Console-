using System;

namespace BlackJackBusinessLogic.Interfaces.Models
{
    public interface IUser:IPlayer
    {
        int Bet { get; set; }
        string Name { get; set; }
    }
}
