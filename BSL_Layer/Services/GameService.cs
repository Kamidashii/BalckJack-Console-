using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using BSL_Layer.Interfaces;
using BSL_Layer.Models;
using DA_Layer;
using DA_Layer.Repositories;
using HelpfulValues.Constants;

namespace BSL_Layer.Services
{
    public class GameService
    {
        #region Services
        public BasicService BasicService { get; private set; }
        public BotService BotService { get; private set; }
        public UserService UserService { get; private set; }
        public CroupierService CroupierService { get; private set; }
        public DeckService DeckService { get; private set; }
        public JSonUnitOfWork JSonService { get; private set; }
        #endregion

        private List<IPlayer> players;
        private List<Deck> decks;
        private IPlayer croupier;

        public List<IPlayer> Players { get { return this.players; } }
        public IPlayer Croupier { get { return this.croupier; } }

        private int decksCount;

        public int gamesCount;
        public int gameId;

        public GameService(List<IPlayer> players, IPlayer croupier, int gamesCount, int decksCount)
        {
            this.players = players;
            this.croupier = croupier;
            this.decks = new List<Deck>(decksCount);
            InitializeManagers();


            GenerateDecks(decksCount);
            this.decksCount = decksCount;

            this.gamesCount = gamesCount;
            gameId = 0;
        }

        private void InitializeManagers()
        {
            this.BasicService = new BasicService(players, decks, croupier);
            this.BotService = new BotService(players, decks, croupier);
            this.UserService = new UserService(players, decks, croupier);
            this.CroupierService = new CroupierService(players, decks, croupier);
            this.DeckService = new DeckService();
            this.JSonService = new JSonUnitOfWork();
        }

        public void SaveResults()
        {
            this.JSonService.Save();
        }

        public List<GameResult> LoadResults()
        {
            List<DA_Layer.Models.GameResult> DAresults = this.JSonService.GameResultsRepository.GetAll().ToList();

            List<BSL_Layer.Models.GameResult> BSLresults = new List<GameResult>();

            for (int i = 0; i < DAresults.Count; ++i)
            {
                BSLresults.Add(new GameResult(DAresults[i]));
            }

            return BSLresults;
        }

        private void GenerateDecks(int decksCount)
        {
            for (int i = 0; i < decksCount; ++i)
            {
                Deck deck = new Deck();
                DeckService.SetAllCards(deck);
                DeckService.ShuffleCards(deck);

                this.decks.Add(deck);
            }
        }
        
        public GameResult CheckWinners()
        {
            GameResult gameResult = new GameResult(gameId);
            gameResult.AllGamesCount = this.gamesCount;

            for (int i = 0; i < this.players.Count; ++i)
            {
                if (BasicService.IsPlayerWonScore(players[i]) && BasicService.IsPlayerWonScore(croupier))
                {
                    UserDraw(players[i], gameResult);
                }

                else if (!BasicService.IsPlayerScoreValid(players[i]) || (players[i].Score <= croupier.Score && BasicService.IsPlayerScoreValid(croupier)))
                {
                    UserLost(players[i], gameResult);
                }

                else if (players[i].Score > croupier.Score || !BasicService.IsPlayerScoreValid(croupier))
                {
                    UserWon(players[i], gameResult);
                }
            }
            gameResult.Croupier = CroupierService.MakePlayerClone(croupier as Player) as Croupier;
            this.JSonService.GameResultsRepository.Create(gameResult.ConvertToDB());
            gameId++;

            return gameResult;
        }

        public void GiveFirstCards()
        {
            for (int i = 0; i < players.Count; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    UserService.PlayerGetCard(players[i], BasicService.PullOutCard());
                    BasicService.RecalculateScore(this.players[i]);
                }
            }

            CroupierService.PlayerGetCard(croupier, BasicService.PullOutCard());
        }

        private void UserWon(IPlayer user, GameResult gameResult)
        {
            if (user.IsBot)
            {
                gameResult.Winners.Add(BotService.MakePlayerClone(user as Player) as Bot);
            }
            else
            {
                gameResult.Winners.Add(UserService.MakePlayerClone(user as Player) as User);
            }
        }

        private void UserLost(IPlayer user, GameResult gameResult)
        {
            if (user.IsBot)
            {
                gameResult.Losers.Add(BotService.MakePlayerClone(user as Player) as Bot);
            }
            else
            {
                gameResult.Losers.Add(UserService.MakePlayerClone(user as Player) as User);
            }
        }

        private void UserDraw(IPlayer user, GameResult gameResult)
        {
            if (user.IsBot)
            {
                gameResult.Draws.Add(BotService.MakePlayerClone(user as Player) as Bot);
            }
            else
            {
                gameResult.Draws.Add(UserService.MakePlayerClone(user as Player) as User);
            }
        }



        private void ResetDecks()
        {
            GenerateDecks(decksCount);
        }


        public void ResetGameData()
        {
            for (int i = 0; i < this.players.Count; ++i)
            {
                BasicService.ResetPlayerScore(this.players[i]);
                BasicService.ResetPlayerDeck(this.players[i]);
            }

            BasicService.ResetPlayerScore(this.croupier);
            BasicService.ResetPlayerDeck(this.croupier);


            ResetDecks();
        }



    }
}
