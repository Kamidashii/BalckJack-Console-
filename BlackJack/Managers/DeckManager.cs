using BlackJack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Managers
{
    public class DeckManager
    {
        public void SetAllCards(Deck deck)
        {
            var suits = Enum.GetValues(typeof(Enums.Card_Enums.CardSuit));
            var ranks = Enum.GetValues(typeof(Enums.Card_Enums.CardRank));

            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < ranks.Length; ++j)
                {
                    Enums.Card_Enums.CardSuit suit = (Enums.Card_Enums.CardSuit)suits.GetValue(i);
                    Enums.Card_Enums.CardRank rank = (Enums.Card_Enums.CardRank)ranks.GetValue(j);

                    Card card;
                    if (rank == Enums.Card_Enums.CardRank.Ace)
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
                int r = i + random.Next(count - i);

                var tmp = deck.Cards[i];
                deck.Cards[i] = deck.Cards[r];
                deck.Cards[r] = tmp;
            }
        }
    }
}
