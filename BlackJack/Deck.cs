using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Deck
    {
        private const int CARDS_COUNT = 52;
        private Random random;
        List<Card> cards = new List<Card>(CARDS_COUNT);

        public Deck()
        {
            random = new Random();

            SetAllCards();
            ShuffleCards();
        }

        public Card TakeCard()
        {
            Card card = this.cards.First();
            this.cards.Remove(this.cards.First());

            return card;
        }

        public void SetAllCards()
        {
            var suits = Enum.GetValues(typeof(Card.CardSuit));
            var ranks = Enum.GetValues(typeof(Card.CardRank));

            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < ranks.Length; ++j)
                {
                    Card.CardSuit suit = (Card.CardSuit)suits.GetValue(i);
                    Card.CardRank rank = (Card.CardRank)ranks.GetValue(j);

                    Card card = new Card(rank, suit);

                    this.cards.Add(card);
                }
            }
        }

        public void ShuffleCards()
        {
            int count = this.cards.Count;
            for (int i = 0; i < count; ++i)
            {
                int r = i + random.Next(count - i);

                var tmp = this.cards[i];
                this.cards[i] = this.cards[r];
                this.cards[r] = tmp;
            }
        }
    }
}
