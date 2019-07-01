using System;
using System.Collections.Generic;
using System.Linq;
using BlackJackBusinessLogic.Interfaces;
using BlackJackBusinessLogic.Interfaces.Models;
using BlackJackBusinessLogic.Interfaces.Services;
using BlackJackBusinessLogic.Mappers;
using BlackJackBusinessLogic.Models;
using BlackJackDataAccess;
using BlackJackDataAccess.Services;

namespace BlackJackBusinessLogic.Services
{
    public class GameService:IGameService
    {
        
        private int _decksCount;
        private BlackJackDataAccess.Interfaces.Services.IJsonService _jsonService;

        public IBasicService BasicService { get; set; }
        public IBotService BotService { get; set; }
        public IBasicService UserService { get; set; }
        public ICroupierService CroupierService { get; set; }
        public IDeckService DeckService { get; set; }


        public List<Interfaces.Models.IUser> Players { get; set; }
        public Interfaces.Models.IPlayer Croupier { get; set; }
        public List<IDeck> Decks { get; set; }

        public int GamesCount { get; set; }
        public int GameId { get; set; }

        public GameService(List<Interfaces.Models.IUser> players, Interfaces.Models.IPlayer croupier, int gamesCount, int decksCount)
        {
            _jsonService = new JsonService();
            DeckService = new DeckService();
            BasicService = new BaseService();
            BotService = new BotService();
            UserService = new UserService();
            CroupierService = new CroupierService();

            Players = players;
            Croupier = croupier;
            Decks = new List<IDeck>(decksCount);

            GenerateDecks(decksCount);
            _decksCount = decksCount;

            GamesCount = gamesCount;
        }

        public void SaveResults()
        {
            _jsonService.Save();
        }

        public List<GameResult> LoadResults()
        {
            var DataAccessResults = _jsonService.GameResultsRepository.GetAll().ToList();

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
                var deck = new Deck();
                DeckService.SetAllCards(deck);
                DeckService.ShuffleCards(deck);

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
            gameResult.Croupier = CroupierService.MakePlayerClone(Croupier as IPlayer) as Croupier;

            _jsonService.GameResultsRepository.Create(BasicService.GameResultMapper.ConvertItemToDataAccess(gameResult));
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
                    pullOutedCard = DeckService.PullOutCard(Decks);
                    UserService.PlayerGetCard(Players[i], pullOutedCard);
                    BasicService.RecalculateScore(this.Players[i]);
                }
            }

            pullOutedCard = DeckService.PullOutCard(Decks);
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
            for (int i = 0; i < Players.Count; ++i)
            {
                BasicService.ResetPlayerScore(Players[i]);
                BasicService.ResetPlayerDeck(Players[i]);
            }

            BasicService.ResetPlayerScore(Croupier);
            BasicService.ResetPlayerDeck(Croupier);


            ResetDecks();
        }

        public void RemoveOldUser()
        {
            for(int i=0;i<Players.Count;++i)
            {
                if(!Players[i].IsBot)
                {
                    Players.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
