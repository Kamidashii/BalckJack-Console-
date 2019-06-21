using System;
using System.Collections.Generic;
using BlackJack_BSL.Interfaces.Models;
using BlackJack_BSL.Models;


namespace BlackJack_BSL.Services
{
    public class UserService : BasicService
    {
        public UserService(List<Interfaces.Models.IUser> players, List<IDeck> decks, Interfaces.Models.IPlayer croupier) : base(players, decks, croupier) { }
        

        public override Interfaces.Models.IPlayer MakePlayerClone(Interfaces.Models.IPlayer original)
        {
            Interfaces.Models.IUser origin = original as IUser;
            Interfaces.Models.IUser copy = new User(origin.Name, origin.Bet, origin.Score, origin.Cards,false);
            return copy;
        }
    }
}
