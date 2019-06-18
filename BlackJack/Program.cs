using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Views;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            GameController.GetInstance().StartGames();

            Console.Read();
        }
    }
}
