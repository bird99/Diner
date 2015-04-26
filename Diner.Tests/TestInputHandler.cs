using Diner.Core;
using Diner.Core.Input;
using Diner.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Diner.Tests
{
    [TestClass]
    public class TestInputHandler
    {
        #region Test: ParseTimeOfDay

        [TestMethod]
        public void Test_ParseTimeOfDay_For_Empty_String_Fails()
        {
            // Arrange
            string userInput = string.Empty;

            var inputHandler = GetInputHandler(userInput);

            // Act
            var timeOfDayResult =inputHandler.ParseTimeOfDay();

            // Assert
            Assert.IsTrue(timeOfDayResult.IsFailure);
        }

        [TestMethod]
        public void Test_ParseTimeOfDay_For_Invalid_String_Fails()
        {
            // Arrange
            const string userInput = "brunch,5,2,something,else";

            var inputHandler = GetInputHandler(userInput);

            // Act
            var timeOfDayResult =inputHandler.ParseTimeOfDay();

            // Assert
            Assert.IsTrue(timeOfDayResult.IsFailure);
        }

        [TestMethod]
        public void Test_ParseTimeOfDay_For_Morning_Succeeds()
        {
            // Arrange
            const string userInput = "morning,5,2,something,else";

            var inputHandler = GetInputHandler(userInput);

            // Act
            var timeOfDayResult =inputHandler.ParseTimeOfDay();

            // Assert
            AssertTimeOfDayMatches(timeOfDayResult, TimeOfDay.Morning);
        }

        [TestMethod]
        public void Test_ParseTimeOfDay_For_Night_Succeeds()
        {
            // Arrange
            const string userInput = "night,5,2,something,else";

            var inputHandler = GetInputHandler(userInput);

            // Act
            var timeOfDayResult =inputHandler.ParseTimeOfDay();

            // Assert
            AssertTimeOfDayMatches(timeOfDayResult, TimeOfDay.Night);
        }        
        
        [TestMethod]
        public void Test_ParseTimeOfDay_For_Night_Is_Not_Case_Sensitive()
        {
            // Arrange
            const string userInputNight = "NiGHT,5,2,something,else";

            var inputHandler = GetInputHandler(userInputNight);

            // Act
            var timeOfDayResult = inputHandler.ParseTimeOfDay();

            // Assert
            AssertTimeOfDayMatches(timeOfDayResult, TimeOfDay.Night);
        }

        [TestMethod]
        public void Test_ParseTimeOfDay_For_Morning_Is_Not_Case_Sensitive()
        {
            // Arrange
            const string userInputNight = "MoRNING,5,2,something,else";

            var inputHandler = GetInputHandler(userInputNight);

            // Act
            var timeOfDayResult =inputHandler.ParseTimeOfDay();

            // Assert
            AssertTimeOfDayMatches(timeOfDayResult, TimeOfDay.Morning);
        }      
        
        [TestMethod]
        public void Test_ParseTimeOfDay_For_Night_Trims_White_Space()
        {
            // Arrange
            const string userInputNight = " night ,5,2,something,else";

            var inputHandler = GetInputHandler(userInputNight);

            // Act
            var timeOfDayResult =inputHandler.ParseTimeOfDay();

            // Assert
            AssertTimeOfDayMatches(timeOfDayResult, TimeOfDay.Night);
        }      
        
        [TestMethod]
        public void Test_ParseTimeOfDay_For_Morning_Trims_White_Space()
        {
            // Arrange
            const string userInputNight = " morning ,5,2,something,else";

            var inputHandler = GetInputHandler(userInputNight);

            // Act
            var timeOfDayResult =inputHandler.ParseTimeOfDay();

            // Assert
            AssertTimeOfDayMatches(timeOfDayResult, TimeOfDay.Morning);
        }

        #endregion

        #region Test: GetNextDishType

        [TestMethod]
        public void Test_GetNextDishType_For_Night_Succeeds()
        {
            // Arrange
            const string userInput = "night,1,2,3,4";

            var inputHandler = GetInputHandler(userInput);

            // Act
            var entreeDishTypeResult = inputHandler.GetNextDishType();
            var sideDishTypeResult = inputHandler.GetNextDishType();
            var drinkDishTypeResult = inputHandler.GetNextDishType();
            var desertDishTypeResult = inputHandler.GetNextDishType();

            // Assert
            AssertDishTypeMatches(entreeDishTypeResult, DishType.Entree);
            AssertDishTypeMatches(sideDishTypeResult, DishType.Side);
            AssertDishTypeMatches(drinkDishTypeResult, DishType.Drink);
            AssertDishTypeMatches(desertDishTypeResult, DishType.Desert);
        }        
        
        [TestMethod]
        public void Test_GetNextDishType_For_Morning_Succeeds()
        {
            // Arrange
            const string userInput = "morning,1,2,3,4";

            var inputHandler = GetInputHandler(userInput);

            // Act
            var entreeDishTypeResult = inputHandler.GetNextDishType();
            var sideDishTypeResult = inputHandler.GetNextDishType();
            var drinkDishTypeResult = inputHandler.GetNextDishType();
            var desertDishTypeResult = inputHandler.GetNextDishType();

            // Assert
            AssertDishTypeMatches(entreeDishTypeResult, DishType.Entree);
            AssertDishTypeMatches(sideDishTypeResult, DishType.Side);
            AssertDishTypeMatches(drinkDishTypeResult, DishType.Drink);
            AssertDishTypeMatches(desertDishTypeResult, DishType.Desert);
        }

        [TestMethod]
        public void Test_GetNextDishType_For_Night_Trims_White_Space()
        {
            // Arrange
            const string userInput = "night, 1,2 , 3 , 4";

            var inputHandler = GetInputHandler(userInput);

            // Act
            var entreeDishTypeResult = inputHandler.GetNextDishType();
            var sideDishTypeResult = inputHandler.GetNextDishType();
            var drinkDishTypeResult = inputHandler.GetNextDishType();
            var desertDishTypeResult = inputHandler.GetNextDishType();

            // Assert
            AssertDishTypeMatches(entreeDishTypeResult, DishType.Entree);
            AssertDishTypeMatches(sideDishTypeResult, DishType.Side);
            AssertDishTypeMatches(drinkDishTypeResult, DishType.Drink);
            AssertDishTypeMatches(desertDishTypeResult, DishType.Desert);
        }             
        
        [TestMethod]
        public void Test_GetNextDishType_For_Morning_Trims_White_Space()
        {
            // Arrange
            const string userInput = "morning, 1,2 , 3 , 4";

            var inputHandler = GetInputHandler(userInput);

            // Act
            var entreeDishTypeResult = inputHandler.GetNextDishType();
            var sideDishTypeResult = inputHandler.GetNextDishType();
            var drinkDishTypeResult = inputHandler.GetNextDishType();
            var desertDishTypeResult = inputHandler.GetNextDishType();

            // Assert
            AssertDishTypeMatches(entreeDishTypeResult, DishType.Entree);
            AssertDishTypeMatches(sideDishTypeResult, DishType.Side);
            AssertDishTypeMatches(drinkDishTypeResult, DishType.Drink);
            AssertDishTypeMatches(desertDishTypeResult, DishType.Desert);
        }

        [TestMethod]
        public void Test_GetNextDishType_Fails_On_Invalid_Item()
        {
            // Arrange
            const string userInput = "night,1,2,3,4,6";

            var inputHandler = GetInputHandler(userInput);

            // Act
            var entreeDishTypeResult = inputHandler.GetNextDishType();
            var sideDishTypeResult = inputHandler.GetNextDishType();
            var drinkDishTypeResult = inputHandler.GetNextDishType();
            var desertDishTypeResult = inputHandler.GetNextDishType();

            var expectedFailureResult = inputHandler.GetNextDishType();

            // Assert
            AssertDishTypeMatches(entreeDishTypeResult, DishType.Entree);
            AssertDishTypeMatches(sideDishTypeResult, DishType.Side);
            AssertDishTypeMatches(drinkDishTypeResult, DishType.Drink);
            AssertDishTypeMatches(desertDishTypeResult, DishType.Desert);

            Assert.IsTrue(expectedFailureResult.IsFailure, "Expected failure for menu item #6");
        }        
        
        [TestMethod]
        public void Test_GetNextDishType_Fails_On_Empty_String()
        {
            // Arrange
            string userInput = string.Empty;

            var inputHandler = GetInputHandler(userInput);

            // Act
            var nextDishTypeResult = inputHandler.GetNextDishType();

            // Assert
            Assert.IsTrue(nextDishTypeResult.IsFailure, "empty user input string, expected parse failure");
        }

        #endregion

        #region Privates

        /// <summary>
        /// Asserts getNextDishTypeResult is successful and matched expectedDishType
        /// </summary>
        /// <param name="getNextDishTypeResult"></param>
        /// <param name="expectedDishType"></param>
        private void AssertDishTypeMatches(OperationResult<DishType> getNextDishTypeResult, DishType expectedDishType)
        {
            // Assert
            Assert.IsTrue(getNextDishTypeResult.IsSuccessful);
            Assert.AreEqual(expectedDishType, getNextDishTypeResult.Item);
        }

        /// <summary>
        /// Asserts if parseTimeOfDayResult is successful and matches expected TimeOfDay
        /// </summary>
        /// <param name="parseTimeOfDayResult"></param>
        /// <param name="expectedTimeOfDay"></param>
        private void AssertTimeOfDayMatches(OperationResult<TimeOfDay> parseTimeOfDayResult, TimeOfDay expectedTimeOfDay)
        {
            // Assert
            Assert.IsTrue(parseTimeOfDayResult.IsSuccessful);
            Assert.AreEqual(expectedTimeOfDay, parseTimeOfDayResult.Item);
        }

        private InputHandler GetInputHandler(string userInput)
        {
            var inputHandler = new InputHandler();

            inputHandler.SetInputString(userInput);

            return inputHandler;
        }

        #endregion
    }
}
