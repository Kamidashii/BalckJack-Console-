using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Models
{
    public class Croupier : Player
    {
        public Croupier()
        {
            this.IsBot = true;
        }
    }
}
