using Diner.Enums;

namespace Diner.Core.Input
{

    /// <summary>
    /// Responsible for parsing user input
    /// </summary>
    public interface IInputHandler
    {
        OperationResult<TimeOfDay> ParseTimeOfDay();

        OperationResult<DishType> GetNextDishType();

        /// <summary>
        /// Returns true/false if the whole input string has been read
        /// </summary>
        /// <returns></returns>
        bool HasMoreMenuItems();
    }
}