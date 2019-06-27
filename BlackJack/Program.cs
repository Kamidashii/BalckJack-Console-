using System;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            GameController gameController = new GameController();
            gameController.StartGames();

            Console.Read();
        }
    }
}
