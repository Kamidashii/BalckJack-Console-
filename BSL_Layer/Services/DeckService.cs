using System;
using BlackJack_BSL.Interfaces;
using BlackJack_BSL.Models;
using Common.Enums;

namespace BlackJack_BSL.Services
{
    public class DeckService
    {
        public void SetAllCards(Deck deck)
        {
            var suits = Enum.GetValues(typeof(CardSuits.CardSuit));
            var ranks = Enum.GetValues(typeof(CardRanks.CardRank));

            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < ranks.Length; ++j)
                {
                    CardSuits.CardSuit suit = (CardSuits.CardSuit)suits.GetValue(i);
                    CardRanks.CardRank rank = (CardRanks.CardRank)ranks.GetValue(j);

                    ICard card;
                    if (rank == CardRanks.CardRank.Ace)
                    {
                        card = new Ace(rank, suit);
                    }
                    else
                    {
                        card = new Card(rank, suit);
                    }

                    deck.Cards.Add(card);
                }
            }
        }

        public void ShuffleCards(Deck deck)
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
