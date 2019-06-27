using System;
using System.Collections.Generic;
using Views;
using System.Threading;
using BlackJackBusinessLogic.Services;
using BlackJackBusinessLogic.Models;
using Common.Enums;
using BlackJackBusinessLogic.Interfaces.Models;
using BlackJackBusinessLogic.Interfaces.Services;

namespace BlackJack
{
    public class GameController
    {
        private IGameService _gameService;

        public GameController()
        {
            var players = new List<IUser>() {
            new Bot("CalmBot",50,BotDemeanors.BotDemeanor.Safe),
            new Bot("NormalBot",100,BotDemeanors.BotDemeanor.Normal),
            new Bot("DesperateBot",400,BotDemeanors.BotDemeanor.Desperate),
            new Bot("NormalBot2", 100,BotDemeanors.BotDemeanor.Normal)};


            IPlayer croupier = new Croupier();
            
            this._gameService = new GameService(players, croupier, 1, 1);
            
            this._gameService.SetServices(
                new BasicService(players, _gameService.Decks, croupier),
                new BotService(players, _gameService.Decks, croupier),
                new UserService(players, _gameService.Decks, croupier),
                new CroupierService(players, _gameService.Decks, croupier)
                );
        }

        public void StartGames()
        {
            if (!Authorize()) return;
            
            for (int i = 0; i < _gameService.GamesCount; ++i)
            {
                MainView.ShowWaitNextGame();
                MainView.ShowGameId(_gameService.GameId);

                StartGame();
            }

            _gameService.SaveResults();
            MainView.ShowGamesOverMessage();

            if (MainView.AskResults())
            {
                List<GameResult> gameResults = _gameService.LoadResults();
                MainView.ShowAllGamesResults(gameResults);
            }
        }

        private void StartGame()
        {
            _gameService.GiveFirstCards();
            ShowPlayersAndCroupierScore();

            StartAllPlayersTurns();

            MainView.ShowCroupierGetTurn();
            _gameService.CroupierService.StartCroupierTurn(_gameService.Croupier);

            MainView.ShowCroupierScore(_gameService.Croupier);
            GameResult gameResult = _gameService.CheckWinners();

            ShowGameResult(gameResult);

            _gameService.ResetGameData();
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
            ICard pullOutedCard = _gameService.BasicService.PullOutCard();

            _gameService.UserService.PlayerGetCard(user, pullOutedCard);

            MainView.ShowUserCardGetting(user);
            MainView.ShowUserSpecificCardGetting(user);

            _gameService.BasicService.RecalculateScore(user);
            MainView.ShowUserScore(user);

            if (!_gameService.BasicService.IsPlayerScoreValid(user))
            {
                MainView.ShowOverfeedScoreMessage(user);
                choosedAction = UserActions.ActionType.Finished;
                return;
            }

            if (_gameService.BasicService.IsPlayerWonScore(user))
            {
                MainView.ShowGreatScoreMessage(user);
                choosedAction = UserActions.ActionType.Finished;
                return;
            }
        }

        private void StartAllPlayersTurns()
        {
            for (int i = 0; i < _gameService.Players.Count; ++i)
            {
                SetArtificialWaiting();

                if (_gameService.Players[i].IsBot)
                {
                    StartBotTurn(_gameService.Players[i] as IBot);
                    return;
                }
                if (!_gameService.Players[i].IsBot)
                {
                    MainView.ShowUserTurn(_gameService.Players[i] as IUser);
                    StartUserTurn(_gameService.Players[i] as IUser);
                    return;
                }
                
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
            for (int i = 0; i < _gameService.Players.Count; ++i)
            {
                MainView.ShowUserScore(_gameService.Players[i] as IUser);
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


        private void StartBotTurn(IBot bot)
        {
            MainView.ShowBotTurn(bot);
            _gameService.BotService.StartBotTurn(bot);
            MainView.ShowBotSpecificCardGetting(bot);
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
            Thread.Sleep(200);
        }

        public bool Authorize()
        {
            MainView.GetLoginAndPassword(out string login, out string password);
            Profile profile = new Profile(login, password);

            if (isProfileExist(profile, out IUser user))
            {
                this._gameService.Players.Insert(0, user);
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
            user = _gameService.BasicService.GetPlayerByProfile(profile);
            return (user != null);
        }
    }
}
