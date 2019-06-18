using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSL_Layer.Models;
using HelpfulValues.Constants;
using HelpfulValues.Enums;

namespace BSL_Layer.Services
{
    public class CroupierService : BasicService
    {
        public CroupierService(List<User> players, List<Deck> decks, Croupier croupier) : base(players, decks, croupier) { }

        public void StartCroupierTurn(Croupier croupier)
        {
            CroupierAction(croupier);
        }

        public void CroupierAction(Croupier croupier)
        {
            do
            {
                CroupierGetCard(croupier, PullOutCard());
                RecalculateScore(croupier);

            } while (croupier.Score < Croupier_Constants.TAKE_UNTIL);
        }

        public void CroupierGetCard(Croupier croupier, Card card)
        {
            croupier.Cards.Add(card);
            croupier.Score += card.GetCost();
        }

        public override Player MakePlayerClone(Player original)
        {
            Croupier origin = original as Croupier;
            Player copy = new Croupier(origin.Score, origin.Cards);
            return copy;
        }
    }
}
