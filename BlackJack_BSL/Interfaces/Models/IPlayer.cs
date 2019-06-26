using System;
using System.Collections.Generic;

namespace BlackJack_BSL.Interfaces.Models
{
    public interface IPlayer
    {
        int Score { get; set; }
        bool IsBot { get; set; }
        List<ICard> Cards { get; set; }
    }
}
