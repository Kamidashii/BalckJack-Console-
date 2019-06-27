using BlackJackBusinessLogic.Interfaces.Models;
using BlackJackBusinessLogic.Models;
using BlackJackBusinessLogic.Services;
using BlackJackDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackBusinessLogic.Interfaces.Services
{
    public interface IGameService
    {
        IBasicService BasicService { get; set; }
        IBotService BotService { get; set; }
        IBasicService UserService { get; set; }
        ICroupierService CroupierService { get; set; }

        List<Interfaces.Models.IUser> Players { get; set; }
        Interfaces.Models.IPlayer Croupier { get; set; }
        List<IDeck> Decks { get; set; }

        int GamesCount { get; set; }
        int GameId { get; set; }


        void SaveResults();
        void SetServices(IBasicService basicService, IBotService botService, IBasicService userService, ICroupierService croupierService);
        List<GameResult> LoadResults();
        GameResult CheckWinners();
        void GiveFirstCards();
        void ResetGameData();
    }
}
