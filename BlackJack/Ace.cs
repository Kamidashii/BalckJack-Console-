using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Ace : Card
    {
        public readonly int specialValue=1;
        public bool isSpecialOn=false;

        public Ace(CardRank rank, CardSuit suit) : base(rank, suit)
        {
        }
    }
}
