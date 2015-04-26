using Diner.Enums;

namespace Diner.Core.InputProcessing
{
    public class InputHandler : IInputHandler
    {
        /// <summary>
        /// Array of user input seperated by commaa
        /// </summary>
        private string[] _inputStringParts;

        /// <summary>
        /// Current read position in the _inputStringParts array (used for GetNextDishType)
        /// </summary>
        private int _currentMenuReadIndex;

        public bool HasMoreMenuItems()
        {
            return _inputStringParts != null && _currentMenuReadIndex < _inputStringParts.Length;
        }

        public void SetInputString(string userInput)
        {
            //NOTE: Not a huge fan of this method, this should be a constructor method (to set the inputString)
            // but but that would take a little more work than needed for a quick demo app.

            _inputStringParts = userInput.Split(',');

            //Note: Reset the read menu index to 1, because 0 is for the TimeOfDay
            _currentMenuReadIndex = 1;
        }

        public OperationResult<TimeOfDay> ParseTimeOfDay()
        {
            var result = new OperationResult<TimeOfDay>();

            var hasValidOrderLength = _inputStringParts == null || _inputStringParts.Length < 1;

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

            var currentString = _inputStringParts[_currentMenuReadIndex];

            var parseResult = ParseDishType(currentString);

            _currentMenuReadIndex++;

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

