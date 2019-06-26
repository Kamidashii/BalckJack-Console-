using BlackJack_BSL.Interfaces.Models;
using BlackJack_BSL.Models;
using BlackJack_BSL.Services;
using BlackJack_DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_BSL.Interfaces.Services
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
