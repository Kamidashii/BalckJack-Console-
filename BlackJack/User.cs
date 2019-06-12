using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class User : Player
    {
        protected enum ActionType { Invalid, Take, Enough, Surrender, ShowCards, Finished }

        protected int bet;
        public string Name { get; set; }

        public User(string name, int bet)
        {
            this.Name = name;
            this.bet = bet;
        }

        public override void StartAct()
        {
            ActionType choosedAction = ActionType.Invalid;
            Console.WriteLine(this.Name + " get a turn");
            do
            {
                Console.WriteLine("\n Choose action: \n 1 - Take a card \n 2 - Enough \n 3 - Surrender \n 4 - ShowCards");
                Enum.TryParse(Console.ReadLine(), out choosedAction);

                switch (choosedAction)
                {
                    case ActionType.Take:
                        {
                            GetCard(GameController.GetInstance().PullOutCard());
                            Console.WriteLine($"{Name} taked {cards.Last().ToString()}");

                            RecalculateScore();
                            ShowScore();

                            if (!isValidScore())
                            {
                                Lost();
                                GameController.GetInstance().RemoveUser(this);
                                choosedAction = ActionType.Finished;
                            }

                            else if (CheckWonScore()) choosedAction = ActionType.Finished;
                        }
                        break;
                    case ActionType.Enough:
                        {
                            Console.WriteLine("Enough");
                            choosedAction = ActionType.Finished;
                        }
                        break;
                    case ActionType.Surrender:
                        {
                            Console.WriteLine("Surrender");
                            choosedAction = ActionType.Finished;
                        }
                        break;
                    case ActionType.ShowCards:
                        {
                            ShowOwnedCards();
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("Invalid choose!");
                            choosedAction = ActionType.Invalid;
                        }
                        break;
                }

            } while (choosedAction != ActionType.Finished);
        }

        public override void GetCard(Card card)
        {
            base.GetCard(card);
            Console.WriteLine($"User {this.Name} taked a card");
        }


        public void ShowOwnedCards()
        {
            for(int i=0;i<this.cards.Count;++i)
            {
                Console.WriteLine(cards[i].ToString());
            }
        }

        public bool CheckWonScore()
        {
            return this.score == GameController.MAX_VALID_SCORE;
        }

        public void Won()
        {
            Console.WriteLine($"\n User {this.Name} has won {bet * GameController.BET_RATIO}!");
        }

        public void Lost()
        {
            Console.WriteLine($"\n User {this.Name} has lost {bet}");
        }

        public void Draw()
        {
            Console.WriteLine($"\n User {this.Name} has got a draw!");
        }

        public override void ShowScore()
        {
            Console.WriteLine($"User {this.Name}'s score now is: {score}");
        }
    }
}
