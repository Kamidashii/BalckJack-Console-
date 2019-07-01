using BlackJackBusinessLogic.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackBusinessLogic.Interfaces.Services
{
    public interface ICroupierService:IBasicService
    {
        void StartCroupierTurn(Interfaces.Models.IPlayer croupier,List<IDeck>decks);
    }
}
