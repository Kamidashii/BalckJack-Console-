using System;
using System.Collections.Generic;
using System.Linq;
using BlackJackBusinessLogic.Interfaces;
using BlackJackBusinessLogic.Interfaces.Models;
using BlackJackBusinessLogic.Interfaces.Services;
using BlackJackBusinessLogic.Mappers;
using BlackJackBusinessLogic.Models;
using BlackJackDataAccess;

namespace BlackJackBusinessLogic.Services
{
    public class GameService:IGameService
    {
        
        private int _decksCount;
        private IDeckService _deckService;
        private BlackJackDataAccess.Interfaces.IJsonService _jsonService;

        public IBasicService BasicService { get; set; }
        public IBotService BotService { get; set; }
        public IBasicService UserService { get; set; }
        public ICroupierService CroupierService { get; set; }
        

        public List<Interfaces.Models.IUser> Players { get; set; }
        public Interfaces.Models.IPlayer Croupier { get; set; }
        public List<IDeck> Decks { get; set; }

        public int GamesCount { get; set; }
        public int GameId { get; set; }

        public GameService(List<Interfaces.Models.IUser> players, Interfaces.Models.IPlayer croupier, int gamesCount, int decksCount)
        {
            this._jsonService = new JsonService();
            this._deckService = new DeckService();

            this.Players = players;
            this.Croupier = croupier;
            this.Decks = new List<IDeck>(decksCount);

            GenerateDecks(decksCount);
            this._decksCount = decksCount;

            this.GamesCount = gamesCount;
        }

        public void SetServices(IBasicService basicService,IBotService botService,IBasicService userService,ICroupierService croupierService)
        {
            this.BasicService = basicService;
            this.BotService = botService;
            this.UserService = userService;
            this.CroupierService = croupierService;
        }

        public void SaveResults()
        {
            this._jsonService.Save();
        }

        public List<GameResult> LoadResults()
        {
            List<BlackJackDataAccess.Models.GameResult> DataAccessResults = this._jsonService.GameResultsRepository.GetAll().ToList();

            var BusinessLogicResults = new List<GameResult>();

            for (int i = 0; i < DataAccessResults.Count; ++i)
            {
                BusinessLogicResults.Add(
                    BasicService.GameResultMapper.ConvertItemToBusinessLogic(DataAccessResults[i])
                    );
            }

            return BusinessLogicResults;
        }

        private void GenerateDecks(int decksCount)
        {
            for (int i = 0; i < decksCount; ++i)
            {
                IDeck deck = new Deck();
                _deckService.SetAllCards(deck);
                _deckService.ShuffleCards(deck);

                Decks.Add(deck);
            }
        }

        public GameResult CheckWinners()
        {
            GameResult gameResult = new GameResult(GameId);
            gameResult.AllGamesCount = this.GamesCount;

            for (int i = 0; i < this.Players.Count; ++i)
            {
                if (BasicService.IsPlayerWonScore(Players[i]) && BasicService.IsPlayerWonScore(Croupier))
                {
                    UserDraw(Players[i], gameResult);
                    continue;
                }

                if (!BasicService.IsPlayerScoreValid(Players[i]) || (Players[i].Score <= Croupier.Score && BasicService.IsPlayerScoreValid(Croupier)))
                {
                    UserLost(Players[i], gameResult);
                    continue;
                }

                if (Players[i].Score > Croupier.Score || !BasicService.IsPlayerScoreValid(Croupier))
                {
                    UserWon(Players[i], gameResult);
                    continue;
                }
            }
            gameResult.Croupier = CroupierService.MakePlayerClone(Croupier as Player) as Croupier;

            this._jsonService.GameResultsRepository.Create(BasicService.GameResultMapper.ConvertItemToDataAccess(gameResult));
            GameId++;

            return gameResult;
        }

        public void GiveFirstCards()
        {
            ICard pullOutedCard;

            for (int i = 0; i < Players.Count; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    pullOutedCard = BasicService.PullOutCard();
                    UserService.PlayerGetCard(Players[i], pullOutedCard);
                    BasicService.RecalculateScore(this.Players[i]);
                }
            }

            pullOutedCard = BasicService.PullOutCard();
            CroupierService.PlayerGetCard(Croupier, pullOutedCard);
        }

        private void UserWon(Interfaces.Models.IPlayer user, GameResult gameResult)
        {
            if (user.IsBot)
            {
                gameResult.Winners.Add(BotService.MakePlayerClone(user) as IBot);
                return;
            }
            if (!user.IsBot)
            {
                gameResult.Winners.Add(UserService.MakePlayerClone(user) as IUser);
                return;
            }
        }

        private void UserLost(Interfaces.Models.IPlayer user, GameResult gameResult)
        {
            if (user.IsBot)
            {
                gameResult.Losers.Add(BotService.MakePlayerClone(user) as IBot);
                return;
            }
            if (!user.IsBot)
            {
                gameResult.Losers.Add(UserService.MakePlayerClone(user) as IUser);
                return;
            }
        }

        private void UserDraw(Interfaces.Models.IPlayer user, GameResult gameResult)
        {
            if (user.IsBot)
            {
                gameResult.Draws.Add(BotService.MakePlayerClone(user) as IBot);
                return;
            }
            if (!user.IsBot)
            {
                gameResult.Draws.Add(UserService.MakePlayerClone(user) as IUser);
                return;
            }
        }



        private void ResetDecks()
        {
            GenerateDecks(_decksCount);
        }


        public void ResetGameData()
        {
            for (int i = 0; i < this.Players.Count; ++i)
            {
                BasicService.ResetPlayerScore(this.Players[i]);
                BasicService.ResetPlayerDeck(this.Players[i]);
            }

            BasicService.ResetPlayerScore(this.Croupier);
            BasicService.ResetPlayerDeck(this.Croupier);


            ResetDecks();
        }
    }
}
