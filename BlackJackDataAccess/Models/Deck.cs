using System;
using System.Collections.Generic;
using System.Linq;
using Common.Constants;

namespace BlackJackDataAccess.Models
{
    public class Deck
    {
        public List<Card> Cards = new List<Card>(Deck_Constants.CardsCount);
        
    }
}
