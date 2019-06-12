using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class GameController
    {
        private List<User> users;
        private List<Deck> decks;
        private Random random;
        private Croupier croupier;

        public const int MAX_VALID_SCORE = 21;
        public const float BET_RATIO = 1.5F;

        private static GameController _instance;

        public void SetData(List<User> newPlayers, Croupier croupier, List<Deck> decks)
        {
            this.users = newPlayers;
            this.croupier = croupier;
            this.decks = decks;

            this.random = new Random();
        }

        public static GameController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameController();
                return _instance;
            }
            return _instance;
        }

        public void StartGame()
        {

            GiveFirstCards();

            for (int i = 0; i < users.Count; ++i)
            {
                this.users[i].StartAct();
            }

            croupier.StartAct();
            Console.WriteLine($"\n Croupier score is: {croupier.GetScore()}");
            CheckWinners();
        }

        public Card PullOutCard()
        {
            Deck randomDeck = this.decks[this.random.Next(0, this.decks.Count)];
            Card randomCard = randomDeck.TakeCard();

            return randomCard;
        }

        public void CheckWinners()
        {
            for (int i = 0; i < this.users.Count; ++i)
            {
                if (users[i].GetScore() > MAX_VALID_SCORE || users[i].GetScore() == 0 || users[i].GetScore() < croupier.GetScore())
                {
                    users[i].Lost();
                }

                else if (users[i].GetScore() > croupier.GetScore())
                {
                    users[i].Won();
                }

                else
                {
                    users[i].Draw();
                }
            }
        }

        protected void GiveFirstCards()
        {
            for(int i=0;i<this.users.Count;++i)
            {
                for(int j=0;j<2;++j)
                {
                    this.users[i].GetCard(this.PullOutCard());
                    this.users[i].RecalculateScore();
                }
                this.users[i].ShowScore();
            }

            this.croupier.GetCard(this.PullOutCard());
        }

        public void RemoveUser(User user)
        {
            this.users.Remove(user);
        }
    }
}
