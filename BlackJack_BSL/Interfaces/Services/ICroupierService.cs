using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_BSL.Interfaces.Services
{
    public interface ICroupierService:IBasicService
    {
        void StartCroupierTurn(Interfaces.Models.IPlayer croupier);
    }
}
