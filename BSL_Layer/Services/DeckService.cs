using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSL_Layer.Interfaces;
using BSL_Layer.Models;
using HelpfulValues.Constants;
using HelpfulValues.Enums;

namespace BSL_Layer.Services
{
    public class DeckService
    {
        public void SetAllCards(Deck deck)
        {
            var suits = Enum.GetValues(typeof(Card_Enums.CardSuit));
            var ranks = Enum.GetValues(typeof(Card_Enums.CardRank));

            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < ranks.Length; ++j)
                {
                    Card_Enums.CardSuit suit = (Card_Enums.CardSuit)suits.GetValue(i);
                    Card_Enums.CardRank rank = (Card_Enums.CardRank)ranks.GetValue(j);

                    ICard card;
                    if (rank == Card_Enums.CardRank.Ace)
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
