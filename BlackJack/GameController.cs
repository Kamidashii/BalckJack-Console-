using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Views;
using System.IO;
using System.Threading;
using BSL_Layer.Services;
using BSL_Layer.Models;
using HelpfulValues.Enums;
using BSL_Layer.Interfaces;

namespace BlackJack
{
    public class GameController
    {
        GameService gameService;

        private static GameController _instance;

        public static GameController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameController();
                return _instance;
            }
            return _instance;
        }

        private GameController()
        {
            List<IPlayer> players = new List<IPlayer>() {
            new Bot("CalmBot",50,Bot_Enums.Bot_Demeanor.Safe),
            new Bot("NormalBot",100,Bot_Enums.Bot_Demeanor.Normal),
            new Bot("DesperateBot",400,Bot_Enums.Bot_Demeanor.Desperate),
            new Bot("NormalBot2", 100,Bot_Enums.Bot_Demeanor.Normal)};


            Croupier croupier = new Croupier();

            this.gameService = new GameService(players, croupier, 1, 1);

            User user = Authorize();
            this.gameService.Players.Add(user);
        }

        public void StartGames()
        {
            for (int i = 0; i < gameService.gamesCount; ++i)
            {
                MainView.ShowWaitNextGame();
                MainView.ShowGameId(gameService.gameId);

                StartGame();
            }

            gameService.SaveResults();
            MainView.ShowGamesOverMessage();

            MainView.AskResults(gameService.LoadResults());
        }

        private void StartGame()
        {
            gameService.GiveFirstCards();
            ShowPlayersScore();

            for (int i = 0; i < gameService.Players.Count; ++i)
            {
                if (gameService.Players[i].IsBot)
                {
                    MainView.ShowBotTurn(gameService.Players[i] as Bot);
                    StartBotTurn(gameService.Players[i] as Bot);
                }
                else
                {
                    MainView.ShowUserTurn(gameService.Players[i] as User);
                    StartUserTurn(gameService.Players[i] as User);
                }
                SetArtificialWaiting();
            }

            MainView.ShowCroupierGetTurn();
            gameService.CroupierService.StartCroupierTurn(gameService.Croupier);

            MainView.ShowCroupierScore(gameService.Croupier);
            GameResult gameResult = gameService.CheckWinners();


            ShowWinners(gameResult.Winners);
            ShowLosers(gameResult.Losers);
            ShowDraws(gameResult.Draws);


            gameService.ResetGameData();
        }

        private void StartUserTurn(User user)
        {
            User_Enums.ActionType choosedAction = User_Enums.ActionType.Invalid;
            do
            {
                MainView.ShowUserActionInstruction();
                choosedAction = MainView.FindOutUserAction();

                switch (choosedAction)
                {
                    case User_Enums.ActionType.Take:
                        {
                            gameService.UserService.PlayerGetCard(user, gameService.BasicService.PullOutCard());
                            MainView.ShowUserCardGetting(user);

                            MainView.ShowUserSpecificCardGetting(user);

                            gameService.BasicService.RecalculateScore(user);
                            MainView.ShowUserScore(user);

                            if (!gameService.BasicService.IsPlayerScoreValid(user))
                            {
                                MainView.ShowOverfeedScoreMessage(user);
                                choosedAction = User_Enums.ActionType.Finished;
                            }

                            else if (gameService.BasicService.IsPlayerWonScore(user))
                            {
                                MainView.ShowGreatScoreMessage(user);
                                choosedAction = User_Enums.ActionType.Finished;
                            }
                        }
                        break;
                    case User_Enums.ActionType.Enough:
                        {
                            choosedAction = User_Enums.ActionType.Finished;
                        }
                        break;
                    case User_Enums.ActionType.Surrender:
                        {
                            MainView.ShowUserLost(user);
                            choosedAction = User_Enums.ActionType.Finished;
                        }
                        break;
                    case User_Enums.ActionType.ShowCards:
                        {
                            ShowUserOwnedCards(user);
                        }
                        break;
                    default:
                        {
                            MainView.ShowInvalidChoose();
                            choosedAction = User_Enums.ActionType.Invalid;
                        }
                        break;
                }

            } while (choosedAction != User_Enums.ActionType.Finished);
        }

        private void ShowPlayersScore()
        {
            for (int i = 0; i < gameService.Players.Count; ++i)
            {
                MainView.ShowUserScore(gameService.Players[i] as User);
                SetArtificialWaiting();
            }

            MainView.ShowCroupierCardGetting();
        }

        private void ShowUserOwnedCards(User user)
        {
            for (int i = 0; i < user.Cards.Count; ++i)
            {
                MainView.ShowCard(user.Cards[i]);
            }
        }


        private void StartBotTurn(Bot bot)
        {
            switch (bot.Demeanor)
            {
                case Bot_Enums.Bot_Demeanor.Desperate:
                    {
                        gameService.BotService.DesperateBotAction(bot);
                        MainView.ShowBotSpecificCardGetting(bot);
                    }
                    break;
                case Bot_Enums.Bot_Demeanor.Normal:
                    gameService.BotService.NormalBotAction(bot);
                    MainView.ShowBotSpecificCardGetting(bot);
                    break;
                case Bot_Enums.Bot_Demeanor.Safe:
                    gameService.BotService.SafeBotAction(bot);
                    MainView.ShowBotSpecificCardGetting(bot);
                    break;
                default:
                    break;
            }
        }

        private void ShowWinners(List<User> users)
        {
            for (int i = 0; i < users.Count; ++i)
            {
                MainView.ShowUserWon(users[i]);
            }
        }

        private void ShowLosers(List<User> users)
        {
            for (int i = 0; i < users.Count; ++i)
            {
                MainView.ShowUserLost(users[i]);
            }
        }

        private void ShowDraws(List<User> users)
        {
            for (int i = 0; i < users.Count; ++i)
            {
                MainView.ShowUserDraw(users[i]);
            }
        }

        private void SetArtificialWaiting()
        {
            Thread.Sleep(200);
        }

        public User Authorize()
        {
            User player;
            do
            {
                string login;
                string password;
                MainView.GetLoginAndPassword(out login, out password);
                Profile profile = new Profile(login, password);
                player = gameService.BasicService.GetPlayerByProfile(profile);
                if (player == null) MainView.IncorrectLoginOrPassword();

            } while (player == null);

            MainView.WelcomeUser(player.Name);
            return player;
        }
    }
}
