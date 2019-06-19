using BSL_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSL_Layer.Interfaces
{
    public interface IPlayerService
    {
        void PlayerGetCard(IPlayer player, ICard card);
    }
}
