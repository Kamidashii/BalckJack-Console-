using System;

namespace BlackJack_BSL.Interfaces
{
    public interface IPlayerService
    {
        void PlayerGetCard(IPlayer player, ICard card);
    }
}
