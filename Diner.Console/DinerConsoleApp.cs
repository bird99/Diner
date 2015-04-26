using System;
using Diner.Core.InputProcessing;
using Diner.Core.OrderProcessing;

namespace Diner
{
    public class DinerConsoleApp
    {
        static void Main(string[] args)
        {
            //TODO: Use IoC in a real app..
            var inputHandler = new InputHandler();
            var orderHandler = new OrderProcessor(inputHandler);

            Console.WriteLine("Welcome to the Diner!");
            Console.WriteLine("You can order off our morning or night menu\n\n");
            Console.WriteLine("Input: time of day (morning or night) followed by a comma seperated list of menu items (1 = entree, 2 = side, 3 = drink, 4 = desert)\n\n");

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

                var resultString = orderHandler.ProcesOrder(userInput);

                Console.WriteLine(resultString);
            }
        }
    }
}
