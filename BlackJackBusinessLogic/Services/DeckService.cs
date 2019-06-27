using System;
using BlackJackBusinessLogic.Interfaces;
using BlackJackBusinessLogic.Interfaces.Models;
using BlackJackBusinessLogic.Interfaces.Services;
using BlackJackBusinessLogic.Models;
using Common.Enums;

namespace BlackJackBusinessLogic.Services
{
    public class DeckService:IDeckService
    {
        public void SetAllCards(IDeck deck)
        {
            var suits = Enum.GetValues(typeof(CardSuits.CardSuit));
            var ranks = Enum.GetValues(typeof(CardRanks.CardRank));

            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < ranks.Length; ++j)
                {
                    CardSuits.CardSuit suit = (CardSuits.CardSuit)suits.GetValue(i);
                    CardRanks.CardRank rank = (CardRanks.CardRank)ranks.GetValue(j);

                    Interfaces.Models.ICard card;
                    CheckAce(out card, suit, rank);

                    deck.Cards.Add(card);
                }
            }
        }

        private void CheckAce(out Interfaces.Models.ICard card, CardSuits.CardSuit suit, CardRanks.CardRank rank)
        {
            if (rank == CardRanks.CardRank.Ace)
            {
                card = new Ace(rank, suit);
                return;
            }
            card = new Card(rank, suit);
        }

        public void ShuffleCards(IDeck deck)
        {
            Random random = new Random();
            int count = deck.Cards.Count;
            for (int i = 0; i < count; ++i)
            {
                int swapIndex = i + random.Next(count - i);

                var tmp = deck.Cards[i];
                deck.Cards[i] = deck.Cards[swapIndex];
                deck.Cards[swapIndex] = tmp;
            }
        }
    }
}
