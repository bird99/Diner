using System;
using Diner.Core;
using Diner.Core.InputProcessing;
using Diner.Core.OrderProcessing;
using Diner.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Diner.Tests
{
    [TestClass]
    public class TestOrderProcessor
    {
        //NOTE: these tests could be more robust, but I wanted to at least show Mocking and setting up Expectations 

        private IInputHandler _inputHandler;

        #region Initialize / Cleanup

        [TestInitialize]
        public void TestInitialize()
        {
            _inputHandler = MockRepository.GenerateStub<IInputHandler>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _inputHandler = null;
        }

        #endregion

        #region Tests

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Null_InputHandler_Throws_Exception()
        {
            var model = new OrderProcessor(null);
        }

        [TestMethod]
        public void Test_ProcessOrder_Fails_On_TimeOfDay_Parsing_Failure()
        {
            // Arrange
            const string userInput = "error";
            const string expectedFailureString = "Time of day failure";

            var failureResult = new OperationResult<TimeOfDay>();
            failureResult.SetAsFail(expectedFailureString);

            InputHandler_Expect_SetInputString(userInput);
            InputHandler_Expect_ParseTimeOfDay(failureResult);

            var orderProcessor = GetOrderProcessor();

            // Act
            var resultString = orderProcessor.ProcesOrder(userInput);

            // Assert
            _inputHandler.VerifyAllExpectations();
            Assert.AreEqual(expectedFailureString, resultString);
        }

        [TestMethod]
        public void Test_ProcessOrder_Succeeds_On_ProcessMenuItems()
        {
            // Arrange
            const string userInput = "night,1,2,3";
            const string expectedParseTimeOfDay = "night";

            var parseTimeOfDayResult = new OperationResult<TimeOfDay>();
            parseTimeOfDayResult.SetAsSuccess(TimeOfDay.Night, expectedParseTimeOfDay);

            var getDishTypeResult = new OperationResult<DishType>();
            getDishTypeResult.SetAsSuccess(DishType.Entree);

            InputHandler_Expect_SetInputString(userInput);
            InputHandler_Expect_ParseTimeOfDay(parseTimeOfDayResult);
            InputHandler_Expect_GetNextDishType(getDishTypeResult);
            InputHandler_Expect_HasMoreMenuItems_Returns_True();

            var orderProcessor = GetOrderProcessor();

            // Act
            var resultString = orderProcessor.ProcesOrder(userInput);

            // Assert
            const string expectedResult = "Steak,Error";
            _inputHandler.VerifyAllExpectations();
            Assert.AreEqual(expectedResult, resultString);
        }
        
        #endregion

        #region Privates

        private OrderProcessor GetOrderProcessor()
        {
            var orderProcessor = new OrderProcessor(_inputHandler);

            return orderProcessor;
        }

        private void InputHandler_Expect_ParseTimeOfDay(OperationResult<TimeOfDay> parseTimeOfDayResult)
        {
            _inputHandler.Expect(x => x.ParseTimeOfDay()).Return(parseTimeOfDayResult);
        }


        private void InputHandler_Expect_GetNextDishType(OperationResult<DishType> getDishTypeResult)
        {
            _inputHandler.Expect(x => x.GetNextDishType()).Return(getDishTypeResult);
        }

        private void InputHandler_Expect_SetInputString(string userInput)
        {
            _inputHandler.Expect(x => x.SetInputString(Arg<string>.Is.Equal(userInput)));
        }

        private void InputHandler_Expect_HasMoreMenuItems_Returns_True()
        {
            _inputHandler.Expect(x => x.HasMoreMenuItems()).Return(true);
        }

        #endregion
    }
}
