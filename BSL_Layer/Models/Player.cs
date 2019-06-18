using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSL_Layer.Models
{
    public abstract class Player
    {
        public int Score = 0;
        public bool IsBot;
        public List<Card> Cards = new List<Card>();
        

        protected void ConvertCards(List<DA_Layer.Models.Card>DAcards)
        {
            this.Cards = new List<Card>(DAcards.Count);
            for (int i = 0; i < DAcards.Count; ++i)
            {
                this.Cards.Add(new Card(DAcards[i]));
            }
        }

        public List<DA_Layer.Models.Card> GetDBCards()
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
