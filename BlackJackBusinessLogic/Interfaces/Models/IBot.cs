using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackBusinessLogic.Interfaces.Models
{
    public interface IBot:IUser
    {
         BotDemeanors.BotDemeanor Demeanor { get; set; }
    }
}
