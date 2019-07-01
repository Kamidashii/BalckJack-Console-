using System;
using System.Collections.Generic;
using BlackJackBusinessLogic.Interfaces.Models;
using BlackJackBusinessLogic.Models;


namespace BlackJackBusinessLogic.Services
{
    public class UserService : BaseService
    {
        public override Interfaces.Models.IPlayer MakePlayerClone(Interfaces.Models.IPlayer original)
        {
            var origin = original as IUser;
            var copy = new User(origin.Name, origin.Bet, origin.Score, origin.Cards,false);
            return copy;
        }
    }
}
