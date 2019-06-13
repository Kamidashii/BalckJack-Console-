using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Models
{
    public class User : Player
    {
        public int Bet;
        public string Name { get; set; }

        public User(string name, int bet)
        {
            this.Name = name;
            this.Bet = bet;
            this.IsBot = false;
        }
    }
}
