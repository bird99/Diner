using System;
using Diner.Core.Input;
using Diner.Enums;

namespace Diner.Core.OrderProcessing
{
    public class OrderProcessor : IOrderProcessor
    {
        public void ProcesOrder(string userInput)
        {
            //TODO- Initializing, InputHandler, should be handled by an IoC container
            IInputHandler inputHandler = new InputHandler(userInput);

            var timeOfDayResult = inputHandler.ParseTimeOfDay();

            if (timeOfDayResult.IsFailure)
            {
                Console.WriteLine(timeOfDayResult.Message);
                return;
            }

            ProcessMenuItems(inputHandler, timeOfDayResult.Item);
        }

        #region Privates

        /// <summary>
        /// Creates a new order based on time of day, handles parsing and outputing all menu items
        /// </summary>
        /// <param name="inputHandler"></param>
        /// <param name="timeOfDay"></param>
        private static void ProcessMenuItems(IInputHandler inputHandler, TimeOfDay timeOfDay)
        {
            var order = new OrderModel(timeOfDay);

            while (inputHandler.HasMoreMenuItems())
            {
                var nextDishResult = inputHandler.GetNextDishType();

                if (nextDishResult.IsFailure)
                {
                    OutputOrderError(order);
                    return;
                }

                var addItemResult = order.AddOrderItem(nextDishResult.Item);

                if (addItemResult.IsFailure)
                {
                    OutputOrderError(order);
                    return;
                }
            }

            Console.WriteLine(order.GetOrderString());
        }

        private static void OutputOrderError(OrderModel order)
        {
            Console.WriteLine("{0},Error", order.GetOrderString());
        }

        #endregion
    }
}