using System;
using System.Collections.Generic;
using Views;
using System.Threading;
using BlackJack_BSL.Services;
using BlackJack_BSL.Models;
using Common.Enums;
using BlackJack_BSL.Interfaces;

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
            List<IUser> players = new List<IUser>() {
            new Bot("CalmBot",50,BotDemeanors.BotDemeanor.Safe),
            new Bot("NormalBot",100,BotDemeanors.BotDemeanor.Normal),
            new Bot("DesperateBot",400,BotDemeanors.BotDemeanor.Desperate),
            new Bot("NormalBot2", 100,BotDemeanors.BotDemeanor.Normal)};


            IPlayer croupier = new Croupier();

            this.gameService = new GameService(players, croupier, 1, 1);
        }

        public void StartGames()
        {
            if (!Authorize()) return;
            
            for (int i = 0; i < gameService.gamesCount; ++i)
            {
                MainView.ShowWaitNextGame();
                MainView.ShowGameId(gameService.gameId);

                StartGame();
            }

            gameService.SaveResults();
            MainView.ShowGamesOverMessage();

            if (MainView.AskResults())
                MainView.ShowAllGamesResults(gameService.LoadResults());
        }

        private void StartGame()
        {
            gameService.GiveFirstCards();
            ShowPlayersAndCroupierScore();

            StartAllPlayersTurns();

            MainView.ShowCroupierGetTurn();
            gameService.CroupierService.StartCroupierTurn(gameService.Croupier);

            MainView.ShowCroupierScore(gameService.Croupier);
            GameResult gameResult = gameService.CheckWinners();

            ShowGameResult(gameResult);

            gameService.ResetGameData();
        }

        private void StartUserTurn(IUser user)
        {
            UserActions.ActionType choosedAction = UserActions.ActionType.Invalid;

            MainView.ShowUserActionInstruction();
            choosedAction = MainView.FindOutUserAction();

            switch (choosedAction)
            {
                case UserActions.ActionType.Take:
                    {
                        UserTakeCard(user, ref choosedAction);
                    }
                    break;
                case UserActions.ActionType.Enough:
                    {
                        choosedAction = UserActions.ActionType.Finished;
                    }
                    break;
                case UserActions.ActionType.Surrender:
                    {
                        MainView.ShowUserLost(user);
                        choosedAction = UserActions.ActionType.Finished;
                    }
                    break;
                case UserActions.ActionType.ShowCards:
                    {
                        ShowUserOwnedCards(user);
                    }
                    break;
                default:
                    {
                        MainView.ShowInvalidChoose();
                        choosedAction = UserActions.ActionType.Invalid;
                    }
                    break;
            }
            if (choosedAction != UserActions.ActionType.Finished)
                StartUserTurn(user);
        }

        private void UserTakeCard(IUser user, ref UserActions.ActionType choosedAction)
        {
            gameService.UserService.PlayerGetCard(user, gameService.BasicService.PullOutCard());

            MainView.ShowUserCardGetting(user);
            MainView.ShowUserSpecificCardGetting(user);

            gameService.BasicService.RecalculateScore(user);
            MainView.ShowUserScore(user);

            if (!gameService.BasicService.IsPlayerScoreValid(user))
            {
                MainView.ShowOverfeedScoreMessage(user);
                choosedAction = UserActions.ActionType.Finished;
            }

            else if (gameService.BasicService.IsPlayerWonScore(user))
            {
                MainView.ShowGreatScoreMessage(user);
                choosedAction = UserActions.ActionType.Finished;
            }
        }

        private void StartAllPlayersTurns()
        {
            for (int i = 0; i < gameService.Players.Count; ++i)
            {
                if (gameService.Players[i].IsBot)
                {
                    MainView.ShowBotTurn(gameService.Players[i] as Bot);
                    StartBotTurn(gameService.Players[i] as Bot);
                }
                else if (!gameService.Players[i].IsBot)
                {
                    MainView.ShowUserTurn(gameService.Players[i] as User);
                    StartUserTurn(gameService.Players[i] as User);
                }
                SetArtificialWaiting();
            }
        }

        private void ShowGameResult(GameResult gameResult)
        {
            ShowWinners(gameResult.Winners);
            ShowLosers(gameResult.Losers);
            ShowDraws(gameResult.Draws);
        }

        private void ShowPlayersAndCroupierScore()
        {
            for (int i = 0; i < gameService.Players.Count; ++i)
            {
                MainView.ShowUserScore(gameService.Players[i] as User);
                SetArtificialWaiting();
            }

            MainView.ShowCroupierCardGetting();
        }

        private void ShowUserOwnedCards(IUser user)
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
                case BotDemeanors.BotDemeanor.Desperate:
                    {
                        gameService.BotService.DesperateBotAction(bot);
                        MainView.ShowBotSpecificCardGetting(bot);
                    }
                    break;
                case BotDemeanors.BotDemeanor.Normal:
                    gameService.BotService.NormalBotAction(bot);
                    MainView.ShowBotSpecificCardGetting(bot);
                    break;
                case BotDemeanors.BotDemeanor.Safe:
                    gameService.BotService.SafeBotAction(bot);
                    MainView.ShowBotSpecificCardGetting(bot);
                    break;
                default:
                    break;
            }
        }

        private void ShowWinners(List<IUser> users)
        {
            for (int i = 0; i < users.Count; ++i)
            {
                MainView.ShowUserWon(users[i]);
            }
        }

        private void ShowLosers(List<IUser> users)
        {
            for (int i = 0; i < users.Count; ++i)
            {
                MainView.ShowUserLost(users[i]);
            }
        }

        private void ShowDraws(List<IUser> users)
        {
            for (int i = 0; i < users.Count; ++i)
            {
                MainView.ShowUserDraw(users[i]);
            }
        }

        private void SetArtificialWaiting()
        {
            Thread.Sleep(2000);
        }

        public bool Authorize()
        {
            MainView.GetLoginAndPassword(out string login, out string password);
            Profile profile = new Profile(login, password);
            if (isProfileExist(profile, out IUser user))
            {
                this.gameService.Players.Insert(0, user);
                MainView.WelcomeUser(user.Name);
                return true;
            }

            MainView.IncorrectLoginOrPassword();
            if (MainView.AttemptAuthorizeAgain())
                Authorize();
            else
            {
                return false;
            }

            return true;
        }

        public bool isProfileExist(IProfile profile, out IUser user)
        {
            user = gameService.BasicService.GetPlayerByProfile(profile);
            return (user != null);
        }
    }
}
