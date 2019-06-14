using BlackJack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Views;

namespace BlackJack.Managers
{
    public class CroupierManager : BasicManager
    {
        public CroupierManager(List<User> players, List<Deck> decks, Croupier croupier) : base(players, decks, croupier) { }

        public void StartCroupierTurn(Croupier croupier)
        {
            MainView.ShowCroupierGetTurn();
            CroupierAction(croupier);
        }

        public void CroupierAction(Croupier croupier)
        {
            do
            {
                CroupierGetCard(croupier, PullOutCard());
                RecalculateScore(croupier);

            } while (croupier.Score < Constants.Croupier_Constants.TAKE_UNTIL);
        }

        public void CroupierGetCard(Croupier croupier, Card card)
        {
            croupier.Cards.Add(card);
            croupier.Score += card.GetCost();
            MainView.ShowCroupierCardGetting();
        }

        public override Player MakePlayerClone(Player original)
        {
            Croupier origin = original as Croupier;
            Player copy = new Croupier(origin.Score, origin.Cards);
            return copy;
        }
    }
}
