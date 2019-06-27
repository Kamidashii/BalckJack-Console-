using System;
using System.Collections.Generic;
using BlackJackBusinessLogic.Interfaces;
using BlackJackBusinessLogic.Interfaces.Models;
using BlackJackBusinessLogic.Interfaces.Services;
using BlackJackBusinessLogic.Models;
using Common.Constants;

namespace BlackJackBusinessLogic.Services
{
    public class CroupierService : BasicService,ICroupierService
    {
        public CroupierService(List<Interfaces.Models.IUser> players, List<IDeck> decks, Interfaces.Models.IPlayer croupier) : base(players, decks, croupier) { }

        public void StartCroupierTurn(Interfaces.Models.IPlayer croupier)
        {
            CroupierAction(croupier);
        }

        private void CroupierAction(Interfaces.Models.IPlayer croupier)
        {
            ICard pullOutedCard = PullOutCard();

            PlayerGetCard(croupier, pullOutedCard);
            RecalculateScore(croupier);

            if (croupier.Score < Croupier_Constants.TakeUntil) CroupierAction(croupier);
        }

        public override Interfaces.Models.IPlayer MakePlayerClone(Interfaces.Models.IPlayer original)
        {
            Croupier origin = original as Croupier;
            Interfaces.Models.IPlayer copy = new Croupier(origin.Score, origin.Cards);
            return copy;
        }
    }
}
