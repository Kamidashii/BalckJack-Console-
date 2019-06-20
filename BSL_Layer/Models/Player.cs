using BSL_Layer.Interfaces;
using System;
using System.Collections.Generic;

namespace BSL_Layer.Models
{
    public abstract class Player:IPlayer
    {
        public int Score { get; set; } = 0;
        public bool IsBot { get; set; }
        public List<ICard> Cards { get; set; } = new List<ICard>();
        

        public List<ICard> ConvertCardsFromDB(List<DA_Layer.Models.Card>DAcards)
        {
            List<ICard> cards = new List<ICard>(DAcards.Count);
            for (int i = 0; i < DAcards.Count; ++i)
            {
                cards.Add(new Card(DAcards[i]));
            }
            return cards;
        }

        public List<DA_Layer.Models.Card> ConvertCardsToDB()
        {
            List<DA_Layer.Models.Card> DAcards=new List<DA_Layer.Models.Card>();
            for(int i=0;i<this.Cards.Count;++i)
            {
                DAcards.Add(this.Cards[i].GetDBCard());
            }
            return DAcards;
        }
    }

   
}
