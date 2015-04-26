using System;
using Diner.Core.InputProcessing;
using Diner.Enums;

namespace Diner.Core.OrderProcessing
{
    public class OrderProcessor : IOrderProcessor
    {
        private readonly IInputHandler _inputHandler;

        public OrderProcessor(IInputHandler inputHandler)
        {
            if (inputHandler == null)
            {
                throw new ArgumentNullException("inputHandler");
            }

            _inputHandler = inputHandler;
        }

        public string ProcesOrder(string userInput)
        {
            _inputHandler.SetInputString(userInput);

            var timeOfDayResult = _inputHandler.ParseTimeOfDay();

            if (timeOfDayResult.IsFailure)
            {
                return timeOfDayResult.Message;
            }

            var menuProcessingResultString = ProcessMenuItems(timeOfDayResult.Item);

            return menuProcessingResultString;
        }

        #region Privates

        /// <summary>
        /// Creates a new order based on time of day, handles parsing and outputing all menu items
        /// </summary>
        /// <param name="inputHandler"></param>
        /// <param name="timeOfDay"></param>
        private string ProcessMenuItems(TimeOfDay timeOfDay)
        {
            var order = new OrderModel(timeOfDay);

            while (_inputHandler.HasMoreMenuItems())
            {
                var nextDishResult = _inputHandler.GetNextDishType();

                if (nextDishResult.IsFailure)
                {
                    return OutputOrderError(order);
                }

                var addItemResult = order.AddOrderItem(nextDishResult.Item);

                if (addItemResult.IsFailure)
                {
                    return OutputOrderError(order);
                }
            }

            return order.GetOrderString();
        }

        private static string OutputOrderError(OrderModel order)
        {
            return string.Format("{0},Error", order.GetOrderString());
        }

        #endregion
    }
}