using System;
using Common.Enums;

namespace BlackJackBusinessLogic.Interfaces.Models
{
    public interface ICard
    {
        CardRanks.CardRank Rank { get; set; }
        CardSuits.CardSuit Suit { get; set; }
        int Cost { get; }

        void DefineCost();
    }
}
