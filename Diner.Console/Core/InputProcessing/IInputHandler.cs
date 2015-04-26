using Diner.Enums;

namespace Diner.Core.InputProcessing
{

    /// <summary>
    /// Responsible for parsing user input
    /// </summary>
    public interface IInputHandler
    {
        /// <summary>
        /// Set input string to parse
        /// </summary>
        /// <param name="userInput"></param>
        void SetInputString(string userInput);

        /// <summary>
        /// Returns true if GetNextDishType can parse more items
        /// </summary>
        /// <returns></returns>
        bool HasMoreMenuItems();

        /// <summary>
        /// Attempts to parse time of day from input string, this method will return failure operation if parsing fails. SetInputString must be called first
        /// </summary>
        /// <returns></returns>
        OperationResult<TimeOfDay> ParseTimeOfDay();

        /// <summary>
        /// Iterates through user input to get all dish types requested by user, this method will return failure operation if parsing fails. SetInputString must be called first
        /// </summary>
        /// <returns></returns>
        OperationResult<DishType> GetNextDishType();
    }
}