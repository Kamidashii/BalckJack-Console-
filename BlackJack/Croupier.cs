using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Croupier : Player
    {
        private const int TAKE_UNTIL = 17; //maximum score until croupier has to take cards
        

        public override void StartAct()
        {
            Console.WriteLine("Croupier get a turn");
            TakeCards();

        }

        public override void GetCard(Card card)
        {
            base.GetCard(card);
            Console.WriteLine($"Croupier taked a card");
        }

        public void TakeCards()
        {
            do
            {
                this.GetCard(GameController.GetInstance().PullOutCard());
                this.RecalculateScore();

            } while (this.score < TAKE_UNTIL);
        }

        public override void ShowScore()
        {
            Console.WriteLine($"Croupier's score now is: {score}");
        }
    }
}
