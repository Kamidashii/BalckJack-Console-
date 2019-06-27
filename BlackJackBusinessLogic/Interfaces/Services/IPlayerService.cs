using BlackJackBusinessLogic.Interfaces;
using System;

namespace BlackJackBusinessLogic.Interfaces.Services
{
    public interface IPlayerService
    {
        void PlayerGetCard(Models.IPlayer player, Interfaces.Models.ICard card);
    }
}
