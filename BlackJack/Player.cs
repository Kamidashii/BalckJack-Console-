using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    abstract class Player
    {

        protected int score = 0;

        protected List<Card> cards = new List<Card>();

        public abstract void StartAct();

        public virtual int GetScore()
        {
            return score;
        }

        public void RecalculateScore()
        {
            if (isValidScore()) return;

            List<Ace> aces = CountAces();
            for (int i = 0; i < aces.Count && !this.isValidScore(); i++)
            {
                this.score -= aces[i].specialValue;
            }
        }

        public virtual void GetCard(Card card)
        {
            this.cards.Add(card);
            this.score += card.GetCost();
        }

        public abstract void ShowScore();

        protected List<Ace> CountAces()
        {
            List<Ace> aces = new List<Ace>();
            for (int i = 0; i < this.cards.Count; ++i)
            {
                if (cards[i].Rank == Card.CardRank.Ace)
                {
                    Ace ace = cards[i] as Ace;
                    if (!ace.isSpecialOn)
                        aces.Add(cards[i] as Ace);
                    else
                    {
                        ace.isSpecialOn = true;
                    }
                }
            }
            return aces;
        }

        public bool isValidScore() //If Score more then 21 returns false
        {
            return this.score <= GameController.MAX_VALID_SCORE;
        }

    }
}
