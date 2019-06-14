using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Views;
using BlackJack.Models;
using Newtonsoft.Json;
using System.IO;
using BlackJack.Managers;
using System.Threading;

namespace BlackJack
{
    class GameController
    {

        #region Managers
        private BasicManager basicManager;
        private BotManager botManager;
        private UserManager userManager;
        private CroupierManager croupierManager;
        private DeckManager deckManager;
        private ResultsManager resultsManager;
        #endregion

        private List<User> players;
        private List<Deck> decks;
        private Croupier croupier;

        private int decksCount;

        private int gamesCount;
        private int gameId;
        private List<GameResult> gameResults;

        private static GameController _instance;

        public void SetData(List<User> players, Croupier croupier, int gamesCount, int decksCount)
        {
            this.players = players;
            this.croupier = croupier;
            this.decks = new List<Deck>(decksCount);
            InitializeManagers();


            GenerateDecks(decksCount);
            this.decksCount = decksCount;

            this.gamesCount = gamesCount;
            gameResults = new List<GameResult>(gamesCount);
            gameId = 0;
        }

        private void InitializeManagers()
        {
            this.basicManager = new BasicManager(players, decks, croupier);
            this.botManager = new BotManager(players, decks, croupier);
            this.userManager = new UserManager(players, decks, croupier);
            this.croupierManager = new CroupierManager(players, decks, croupier);
            this.deckManager = new DeckManager();
            this.resultsManager = new ResultsManager();
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

        public void StartGames()
        {
            //MainView.AskResults(resultsManager.LoadResults());
            //Console.ReadLine();
            for (int i = 0; i < this.gamesCount; ++i)
            {
                MainView.ShowWaitNextGame();
                MainView.ShowGameId(this.gameId);

                StartGame();
            }

            resultsManager.SaveResults(this.gameResults);
            MainView.ShowGamesOverMessage();

            MainView.AskResults(resultsManager.LoadResults());
        }

        private void StartGame()
        {
            GiveFirstCards();

            for (int i = 0; i < players.Count; ++i)
            {
                if (this.players[i].IsBot)
                {
                    botManager.StartBotTurn(this.players[i] as Bot);
                }
                else
                {
                    userManager.StartUserTurn(this.players[i]);
                }
                SetArtificialWaiting();
            }

            croupierManager.StartCroupierTurn(croupier);
            MainView.ShowCroupierScore(croupier);
            CheckWinners();

            ResetGameData();
        }



        private void GenerateDecks(int decksCount)
        {
            for (int i = 0; i < decksCount; ++i)
            {
                Deck deck = new Deck();
                deckManager.SetAllCards(deck);
                deckManager.ShuffleCards(deck);

                this.decks.Add(deck);
            }
        }



        private void CheckWinners()
        {
            GameResult gameResult = new GameResult(gameId);
            gameResult.AllGamesCount = this.gamesCount;

            for (int i = 0; i < this.players.Count; ++i)
            {
                if (!basicManager.IsPlayerScoreValid(players[i]) || (players[i].Score <= croupier.Score && basicManager.IsPlayerScoreValid(croupier)))
                {
                    UserLost(players[i], gameResult);
                }

                else if (players[i].Score > croupier.Score || !basicManager.IsPlayerScoreValid(croupier))
                {
                    UserWon(players[i], gameResult);
                }

                else
                {
                    UserDraw(players[i], gameResult);
                }
            }
            gameResult.Croupier = croupierManager.MakePlayerClone(croupier as Player) as Croupier;
            gameResults.Add(gameResult);
            gameId++;
        }

        private void GiveFirstCards()
        {
            for (int i = 0; i < players.Count; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    userManager.UserGetCard(players[i], basicManager.PullOutCard());
                    basicManager.RecalculateScore(this.players[i]);
                }
                MainView.ShowUserScore(players[i]);
                SetArtificialWaiting();
            }

            croupierManager.CroupierGetCard(croupier, basicManager.PullOutCard());
        }

        private void UserWon(User user, GameResult gameResult)
        {
            if (user.IsBot)
            {
                gameResult.Winners.Add(botManager.MakePlayerClone(user as Player) as Bot);
            }
            else
            {
                gameResult.Winners.Add(userManager.MakePlayerClone(user as Player) as User);
            }
            MainView.ShowUserWon(user);
        }

        private void UserLost(User user, GameResult gameResult)
        {
            if (user.IsBot)
            {
                gameResult.Losers.Add(botManager.MakePlayerClone(user as Player) as Bot);
            }
            else
            {
                gameResult.Losers.Add(userManager.MakePlayerClone(user as Player) as User);
            }
            MainView.ShowUserLost(user);
        }

        private void UserDraw(User user, GameResult gameResult)
        {
            if (user.IsBot)
            {
                gameResult.Draw.Add(botManager.MakePlayerClone(user as Player) as Bot);
            }
            else
            {
                gameResult.Draw.Add(userManager.MakePlayerClone(user as Player) as User);
            }
            MainView.ShowUserDraw(user);
        }

        private void SetArtificialWaiting()
        {
            Thread.Sleep(2000);
        }


        private void ResetDecks()
        {
            GenerateDecks(decksCount);
        }


        private void ResetGameData()
        {
            for (int i = 0; i < this.players.Count; ++i)
            {
                basicManager.ResetPlayerScore(this.players[i]);
                basicManager.ResetPlayerDeck(this.players[i]);
            }

            basicManager.ResetPlayerScore(this.croupier);
            basicManager.ResetPlayerDeck(this.croupier);


            ResetDecks();
        }

    }
}
