using System;
using Diner.Core.OrderProcessing;

namespace Diner
{
    public class DinerConsoleApp
    {
        static void Main(string[] args)
        {
            //TODO: use IoC container
            var orderHandler = new OrderProcessor();

            Console.WriteLine("Welcome to the Diner!");
            Console.WriteLine("You can order off our morning or night menu");
            Console.WriteLine("Input: menu, exit, help");

            while (true)
            {
                Console.WriteLine("What would you like to eat?");

                var userInput = Console.ReadLine();

                if (userInput == null)
                {
                    continue;
                }

                userInput = userInput.ToLower();

                if (userInput == "exit")
                {
                    break;
                }

                orderHandler.ProcesOrder(userInput);
            }
        }
    }
}
