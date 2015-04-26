using Diner.Enums;

namespace Diner.Core.Input
{
    public class InputHandler : IInputHandler
    {
        /// <summary>
        /// Array of user input seperated by commaa
        /// </summary>
        private readonly string[] _inputStringParts;

        /// <summary>
        /// Reading the menu items starts with a positon of 1 because index 0 is menu type
        /// </summary>
        private int currentMenuReadIndex = 1;

        public InputHandler(string userInput)
        {
            _inputStringParts = userInput.Split(',');
        }

        public bool HasMoreMenuItems()
        {
            return currentMenuReadIndex < _inputStringParts.Length;
        }

        public OperationResult<TimeOfDay> ParseTimeOfDay()
        {
            var result = new OperationResult<TimeOfDay>();

            var hasValidOrderLength = _inputStringParts.Length < 1;

            if (hasValidOrderLength)
            {
                result.SetAsFail("Error - you must enter time of day and what you would like to order");
                return result;
            }

            const int indexTimeOfDay = 0;

            var timeOfDayString = _inputStringParts[indexTimeOfDay];

            result = ParseTimeOfDay(timeOfDayString);

            return result;
        }

        public OperationResult<DishType> GetNextDishType()
        {
            var result = new OperationResult<DishType>();

            if (!HasMoreMenuItems())
            {
                result.SetAsFail("End of string");
                return result;
            }

            var currentString = _inputStringParts[currentMenuReadIndex];

            var parseResult = ParseDishType(currentString);

            currentMenuReadIndex++;

            return parseResult;
        }

        #region Privates

        private OperationResult<DishType> ParseDishType(string currentString)
        {
            var result = new OperationResult<DishType>();

            currentString = currentString.Trim();

            switch (currentString)
            {
                case "1":
                    result.SetAsSuccess(DishType.Entree);
                    break;
                case "2":
                    result.SetAsSuccess(DishType.Side);
                    break;
                case "3":
                    result.SetAsSuccess(DishType.Drink);
                    break;
                case "4":
                    result.SetAsSuccess(DishType.Desert);
                    break;
                default:
                    result.SetAsFail("Item not found");
                    break;
            }

            return result;
        }

        private OperationResult<TimeOfDay> ParseTimeOfDay(string timeOfDayString)
        {
            var result = new OperationResult<TimeOfDay>();

            timeOfDayString = timeOfDayString.Trim().ToLower();

            if (timeOfDayString == TimeOfDay.Night.ToString().ToLower())
            {
                result.SetAsSuccess(TimeOfDay.Night);
            }
            else if (timeOfDayString == TimeOfDay.Morning.ToString().ToLower())
            {
                result.SetAsSuccess(TimeOfDay.Morning);
            }
            else
            {
                result.SetAsFail("Could not determine time of day (options: Morning or Night)");
            }

            return result;
        }

        #endregion
    }
}

