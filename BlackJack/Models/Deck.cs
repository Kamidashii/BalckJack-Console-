using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Models
{
    public class Deck
    {
        public List<Card> Cards = new List<Card>(Constants.Deck_Constants.CARDS_COUNT);

        public Card TakeCard()
        {
            Card card = this.Cards.First();
            this.Cards.Remove(this.Cards.First());

            return card;
        }
    }
}
