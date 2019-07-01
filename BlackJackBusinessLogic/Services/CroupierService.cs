using System;
using System.Collections.Generic;
using BlackJackBusinessLogic.Interfaces;
using BlackJackBusinessLogic.Interfaces.Models;
using BlackJackBusinessLogic.Interfaces.Services;
using BlackJackBusinessLogic.Models;
using Common.Constants;

namespace BlackJackBusinessLogic.Services
{
    public class CroupierService : BaseService,ICroupierService
    {
        private IDeckService _deckService;

        public CroupierService()
        {
            _deckService = new DeckService();
        }

        public void StartCroupierTurn(Interfaces.Models.IPlayer croupier, List<IDeck> decks)
        {
            CroupierAction(croupier,decks);
        }

        private void CroupierAction(Interfaces.Models.IPlayer croupier,List<IDeck>decks)
        {
            ICard pullOutedCard = _deckService.PullOutCard(decks);

            PlayerGetCard(croupier, pullOutedCard);
            RecalculateScore(croupier);

            if (croupier.Score < Croupier_Constants.TakeUntil)
            {
                CroupierAction(croupier,decks);
            }
        }

        public override Interfaces.Models.IPlayer MakePlayerClone(Interfaces.Models.IPlayer original)
        {
            var origin = original as Croupier;
            var copy = new Croupier(origin.Score, origin.Cards);
            return copy;
        }
    }
}
