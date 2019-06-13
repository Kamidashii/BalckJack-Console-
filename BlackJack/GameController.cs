using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Views;
using BlackJack.Models;
using Newtonsoft.Json;
using System.IO;

namespace BlackJack
{
    class GameController
    {
        private List<User> players;
        private List<Deck> decks;
        private Random random;
        private Croupier croupier;

        private int decksCount;

        private int gamesCount;
        private int gameId;
        private List<GameResult> gameResults;


        private MainView mainView;


        private static GameController _instance;

        public void SetData(List<User> players, Croupier croupier, int gamesCount, int decksCount, MainView mainView)
        {
            this.players = players;
            this.croupier = croupier;

            GenerateDecks(decksCount);

            this.mainView = mainView;

            this.decksCount = decksCount;

            this.gamesCount = gamesCount;
            gameResults = new List<GameResult>(gamesCount);
            gameId = 0;

            this.random = new Random();
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
            for (int i = 0; i < this.gamesCount; ++i)
            {
                mainView.ShowWaitNextGame();
                mainView.ShowGameId(this.gameId);

                StartGame();
            }

            SaveResults();
            mainView.ShowGamesOverMessage();
        }

        private void StartGame()
        {
            GiveFirstCards();

            for (int i = 0; i < players.Count; ++i)
            {
                if (this.players[i].IsBot)
                {
                    StartBotTurn(this.players[i] as Bot);
                }
                else
                {
                    StartUserTurn(this.players[i]);
                }
            }

            StartCroupierTurn(croupier);
            mainView.ShowCroupierScore(croupier);
            CheckWinners();

            ResetGameData();
        }

        #region PlayersLogic

        #region UserLogic


        private void StartUserTurn(User user)
        {
            Enums.User_Enums.ActionType choosedAction = Enums.User_Enums.ActionType.Invalid;
            mainView.ShowUserTurn(user);
            do
            {
                mainView.ShowUserActionInstruction();
                Enum.TryParse(Console.ReadLine(), out choosedAction);

                switch (choosedAction)
                {
                    case Enums.User_Enums.ActionType.Take:
                        {
                            UserGetCard(user, PullOutCard());

                            mainView.ShowUserSpecificCardGetting(user);

                            RecalculateScore(user);
                            mainView.ShowUserScore(user);

                            if (!IsPlayerScoreValid(user))
                            {
                                mainView.ShowOverfeedScoreMessage(user);
                                choosedAction = Enums.User_Enums.ActionType.Finished;
                            }

                            else if (IsPlayerWonScore(user))
                            {
                                mainView.ShowGreatScoreMessage(user);
                                choosedAction = Enums.User_Enums.ActionType.Finished;
                            }
                        }
                        break;
                    case Enums.User_Enums.ActionType.Enough:
                        {
                            choosedAction = Enums.User_Enums.ActionType.Finished;
                        }
                        break;
                    case Enums.User_Enums.ActionType.Surrender:
                        {
                            mainView.ShowUserLost(user);
                            choosedAction = Enums.User_Enums.ActionType.Finished;
                        }
                        break;
                    case Enums.User_Enums.ActionType.ShowCards:
                        {
                            ShowUserOwnedCards(user);
                        }
                        break;
                    default:
                        {
                            mainView.ShowInvalidChoose();
                            choosedAction = Enums.User_Enums.ActionType.Invalid;
                        }
                        break;
                }

            } while (choosedAction != Enums.User_Enums.ActionType.Finished);
        }

        private void UserGetCard(User user, Card card)
        {
            user.Cards.Add(card);
            user.Score += card.GetCost();

            mainView.ShowUserCardGetting(user);
        }

        private void ShowUserOwnedCards(User user)
        {
            for (int i = 0; i < user.Cards.Count; ++i)
            {
                mainView.ShowCard(user.Cards[i]);
            }
        }

        #endregion

        #region CroupierTurn

        private void StartCroupierTurn(Croupier croupier)
        {
            Console.WriteLine("Croupier get a turn");
            CroupierAction(croupier);
        }

        private void CroupierAction(Croupier croupier)
        {
            do
            {
                PlayerGetCard(croupier, PullOutCard());
                RecalculateScore(croupier);

            } while (croupier.Score < Constants.Croupier_Constants.TAKE_UNTIL);
        }

        public void PlayerGetCard(User user, Card card)
        {
            user.Cards.Add(card);
            user.Score += card.GetCost();
            mainView.ShowUserCardGetting(user);
        }

        public void PlayerGetCard(Croupier croupier,Card card)
        {
            croupier.Cards.Add(card);
            croupier.Score += card.GetCost();
            mainView.ShowCroupierCardGetting();
        }

        public void PlayerGetCard(Bot bot, Card card)
        {
            bot.Cards.Add(card);
            bot.Score += card.GetCost();
            mainView.ShowBotCardGetting(bot);
        }

        #endregion 

        #region Bots
        private void DesperateBotAction(Bot bot)
        {
            while (!IsPlayerWonScore(bot) && IsPlayerScoreValid(bot))
            {
                PlayerGetCard(bot, PullOutCard());
                mainView.ShowBotSpecificCardGetting(bot);
                RecalculateScore(bot);
            } 
        }

        private void NormalBotAction(Bot bot)
        {
            while (bot.Score <= 15 && IsPlayerScoreValid(bot))
            {
                PlayerGetCard(bot, PullOutCard());
                mainView.ShowBotSpecificCardGetting(bot);
                RecalculateScore(bot);
            } 
        }

        private void SafeBotAction(Bot bot)
        {
            while (bot.Score <= 11 && IsPlayerScoreValid(bot))
            {
                PlayerGetCard(bot, PullOutCard());
                mainView.ShowBotSpecificCardGetting(bot);
                RecalculateScore(bot);
            } 
        }

        private void StartBotTurn(Bot bot)
        {
            mainView.ShowBotTurn(bot);

            switch (bot.Demeanor)
            {
                case Enums.Bot_Enums.Bot_Demeanor.Desperate:
                    DesperateBotAction(bot);
                    break;
                case Enums.Bot_Enums.Bot_Demeanor.Normal:
                    NormalBotAction(bot);
                    break;
                case Enums.Bot_Enums.Bot_Demeanor.Safe:
                    SafeBotAction(bot);
                    break;
                default:
                    break;
            }
        }

        #endregion


        private List<Ace> CountAces(Player player)
        {
            List<Ace> aces = new List<Ace>();
            for (int i = 0; i < player.Cards.Count; ++i)
            {
                if (player.Cards[i].Rank == Enums.Card_Enums.CardRank.Ace)
                {
                    Ace ace = player.Cards[i] as Ace;
                    if (!ace.IsSpecialOn)
                        aces.Add(player.Cards[i] as Ace);
                    else
                    {
                        ace.IsSpecialOn = true;
                    }
                }
            }
            return aces;
        }

        private void RecalculateScore(Player player)
        {
            if (IsPlayerScoreValid(player)) return;

            List<Ace> aces = CountAces(player);
            for (int i = 0; i < aces.Count && !IsPlayerScoreValid(player); i++)
            {
                player.Score -= aces[i].GetSpecialCostDifference();
            }
        }

        #endregion

        #region DeckLogic
        private void SetAllCards(Deck deck)
        {
            var suits = Enum.GetValues(typeof(Enums.Card_Enums.CardSuit));
            var ranks = Enum.GetValues(typeof(Enums.Card_Enums.CardRank));

            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < ranks.Length; ++j)
                {
                    Enums.Card_Enums.CardSuit suit = (Enums.Card_Enums.CardSuit)suits.GetValue(i);
                    Enums.Card_Enums.CardRank rank = (Enums.Card_Enums.CardRank)ranks.GetValue(j);

                    Card card;
                    if (rank == Enums.Card_Enums.CardRank.Ace)
                    {
                        card = new Ace(rank, suit);
                    }
                    else
                    {
                        card = new Card(rank, suit);
                    }

                    deck.Cards.Add(card);
                }
            }
        }

        private void ShuffleCards(Deck deck)
        {
            Random random = new Random();
            int count = deck.Cards.Count;
            for (int i = 0; i < count; ++i)
            {
                int r = i + random.Next(count - i);

                var tmp = deck.Cards[i];
                deck.Cards[i] = deck.Cards[r];
                deck.Cards[r] = tmp;
            }
        }
        #endregion


        private void GenerateDecks(int decksCount)
        {
            this.decks = new List<Deck>(decksCount);

            for (int i = 0; i < decksCount; ++i)
            {
                Deck deck = new Deck();
                SetAllCards(deck);
                ShuffleCards(deck);

                this.decks.Add(deck);
            }
        }

        private Card PullOutCard()
        {
            Deck randomDeck = this.decks[this.random.Next(0, this.decks.Count)];
            Card randomCard = randomDeck.TakeCard();

            return randomCard;
        }

        private void CheckWinners()
        {
            GameResult gameResult = new GameResult(gameId);

            for (int i = 0; i < this.players.Count; ++i)
            {
                if (!IsPlayerScoreValid(players[i]) || (players[i].Score < croupier.Score && IsPlayerScoreValid(croupier)))
                {
                    UserLost(players[i], gameResult);
                }

                else if (players[i].Score > croupier.Score || !IsPlayerScoreValid(croupier))
                {
                    UserWon(players[i], gameResult);
                }

                else
                {
                    UserDraw(players[i], gameResult);
                }
            }
            gameResults.Add(gameResult);
            gameId++;
        }

        private void GiveFirstCards()
        {
            for (int i = 0; i < players.Count; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    UserGetCard(players[i], this.PullOutCard());
                    RecalculateScore(this.players[i]);
                }
                mainView.ShowUserScore(players[i]);
            }

            PlayerGetCard(croupier, PullOutCard());
        }

        private void UserWon(User user, GameResult gameResult)
        {
            gameResult.Winners.Add(user);
            mainView.ShowUserWon(user);
        }

        private void UserLost(User user, GameResult gameResult)
        {
            gameResult.Losers.Add(user);
            mainView.ShowUserLost(user);
        }

        private void UserDraw(User user, GameResult gameResult)
        {
            gameResult.Draw.Add(user);
            mainView.ShowUserDraw(user);
        }

        private void SaveResults()
        {
            try
            {
                CreateResultsFolder();

                string jSon = JsonConvert.SerializeObject(this.gameResults);
                using (StreamWriter writer = new StreamWriter(Constants.GameController_Constants.RESULTS_PATH, false, Encoding.Default))
                {
                    writer.Write(jSon);
                }
            }
            catch (Exception exc)
            {
                mainView.ShowExceptionMessage(exc.Message);
            }
        }

        private void CreateResultsFolder()
        {
            if (!Directory.Exists(Constants.GameController_Constants.RESULTS_FOLDER))
            {
                Directory.CreateDirectory(Constants.GameController_Constants.RESULTS_FOLDER);
            }
        }

        private void CreateResultsFile()
        {
            if (!File.Exists(Constants.GameController_Constants.RESULTS_PATH))
            {
                File.Create(Constants.GameController_Constants.RESULTS_PATH);
            }
        }

        private List<GameResult> LoadResults()
        {
            try
            {
                CreateResultsFolder();
                CreateResultsFile();

                using (StreamReader reader = new StreamReader(Constants.GameController_Constants.RESULTS_PATH, Encoding.Default))
                {
                    string jSon = reader.ReadToEnd();

                    return JsonConvert.DeserializeObject<List<GameResult>>(jSon);
                }
            }
            catch (Exception exc)
            {
                mainView.ShowExceptionMessage(exc.Message);
                return null;
            }
        }

        private void ResetPlayerScore(Player player)
        {
            player.Score = 0;
        }

        private void ResetPlayerDeck(Player player)
        {
            player.Cards = new List<Card>();
        }

        private void ResetDecks()
        {
            GenerateDecks(decksCount);
        }


        private void ResetGameData()
        {
            for (int i = 0; i < this.players.Count; ++i)
            {
                ResetPlayerScore(this.players[i]);
                ResetPlayerDeck(this.players[i]);
            }

            ResetPlayerScore(this.croupier);
            ResetPlayerDeck(this.croupier);


            ResetDecks();
        }

        private bool IsPlayerWonScore(Player player)
        {
            return player.Score == Constants.GameController_Constants.MAX_VALID_SCORE;
        }

        private bool IsPlayerScoreValid(Player player) //If Score more then 21 returns false
        {
            return player.Score <= Constants.GameController_Constants.MAX_VALID_SCORE;
        }
    }
}
