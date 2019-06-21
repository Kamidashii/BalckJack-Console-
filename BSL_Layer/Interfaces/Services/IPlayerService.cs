using BlackJack_BSL.Interfaces;
using System;

namespace BlackJack_BSL.Interfaces.Services
{
    public interface IPlayerService
    {
        void PlayerGetCard(Models.IPlayer player, Interfaces.Models.ICard card);
    }
}
