using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackBusinessLogic.Interfaces.Services
{
    public interface IBotService:IBasicService
    {
        void StartBotTurn(Interfaces.Models.IPlayer bot);
    }
}
