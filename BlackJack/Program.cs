using System;

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
