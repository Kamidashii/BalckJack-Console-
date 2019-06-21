using System;
using Common.Enums;

namespace BlackJack_BSL.Interfaces
{
    public interface ICard
    {
        CardRanks.CardRank Rank { get; set; }
        CardSuits.CardSuit Suit { get; set; }
        int Cost { get; }

        void DefineCost();
    }
}
