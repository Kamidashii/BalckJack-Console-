using System;
using System.Collections.Generic;
using BlackJack_BSL.Interfaces;
using BlackJack_BSL.Models;
using Common.Constants;

namespace BlackJack_BSL.Services
{
    public class CroupierService : BasicService
    {
        public CroupierService(List<IUser> players, List<Deck> decks, IPlayer croupier) : base(players, decks, croupier) { }

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

            } while (croupier.Score < Croupier_Constants.TakeUntil);
        }

        public override IPlayer MakePlayerClone(IPlayer original)
        {
            Croupier origin = original as Croupier;
            IPlayer copy = new Croupier(origin.Score, origin.Cards);
            return copy;
        }
    }
}
