using System;
using System.Collections.Generic;
using BSL_Layer.Interfaces;
using BSL_Layer.Models;
using HelpfulValues.Constants;

namespace BSL_Layer.Services
{
    public class CroupierService : BasicService
    {
        public CroupierService(List<IPlayer> players, List<Deck> decks, IPlayer croupier) : base(players, decks, croupier) { }

        public void StartCroupierTurn(IPlayer croupier)
        {
            CroupierAction(croupier);
        }

        public void CroupierAction(IPlayer croupier)
        {
            do
            {
                PlayerGetCard(croupier, PullOutCard());
                RecalculateScore(croupier);

            } while (croupier.Score < Croupier_Constants.TAKE_UNTIL);
        }

        public override IPlayer MakePlayerClone(IPlayer original)
        {
            Croupier origin = original as Croupier;
            IPlayer copy = new Croupier(origin.Score, origin.Cards);
            return copy;
        }
    }
}
