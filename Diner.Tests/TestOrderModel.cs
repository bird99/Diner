using Diner.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Diner.Tests
{
    [TestClass]
    public class TestOrderModel
    {
        #region Test: GetOrderString

        [TestMethod]
        public void Test_GetOrderString_At_Night_Displays_Items_In_Correct_Order()
        {
            // Arrange
            var ordeModel = GetOrderModel(TimeOfDay.Night);

            // Act
            var addDesertResult = ordeModel.AddOrderItem(DishType.Desert);
            var addSideResult = ordeModel.AddOrderItem(DishType.Side);
            var addEntreeResult = ordeModel.AddOrderItem(DishType.Entree);
            var addDrinkResult = ordeModel.AddOrderItem(DishType.Drink);

            // Assert

            //TODO: this could be more robust by using the actual MenuItems rather than hard coding the string for expeced value
            const string expectedDisplayString = "Steak,Potato,Wine,Cake";

            Assert.IsTrue(addEntreeResult.IsSuccessful);
            Assert.IsTrue(addSideResult.IsSuccessful);
            Assert.IsTrue(addDrinkResult.IsSuccessful);
            Assert.IsTrue(addDesertResult.IsSuccessful);
            Assert.AreEqual(expectedDisplayString, ordeModel.GetOrderString());
        }

        [TestMethod]
        public void Test_GetOrderString_At_Night_Handles_Multiple_Sides()
        {
            // Arrange
            var ordeModel = GetOrderModel(TimeOfDay.Night);

            // Act
            var addDesertResult = ordeModel.AddOrderItem(DishType.Desert);
            var addSideResult = ordeModel.AddOrderItem(DishType.Side);
            var addSideSecondResult = ordeModel.AddOrderItem(DishType.Side);
            var addEntreeResult = ordeModel.AddOrderItem(DishType.Entree);
            var addDrinkResult = ordeModel.AddOrderItem(DishType.Drink);

            // Assert

            //TODO: this could be more robust by using the actual MenuItems rather than hard coding the string for expeced value
            const string expectedDisplayString = "Steak,Potato(x2),Wine,Cake";

            Assert.IsTrue(addEntreeResult.IsSuccessful);
            Assert.IsTrue(addSideResult.IsSuccessful);
            Assert.IsTrue(addSideSecondResult.IsSuccessful);
            Assert.IsTrue(addDrinkResult.IsSuccessful);
            Assert.IsTrue(addDesertResult.IsSuccessful);
            Assert.AreEqual(expectedDisplayString, ordeModel.GetOrderString());
        }
        
        [TestMethod]
        public void Test_GetOrderString_At_Mornning_Displays_Items_In_Correct_Order()
        {
            // Arrange
            var ordeModel = GetOrderModel(TimeOfDay.Morning);

            // Act
            var addSideResult = ordeModel.AddOrderItem(DishType.Side);
            var addEntreeResult = ordeModel.AddOrderItem(DishType.Entree);
            var addDrinkResult = ordeModel.AddOrderItem(DishType.Drink);

            // Assert

            //TODO: this could be more robust by using the actual MenuItems rather than hard coding the string for expeced value
            const string expectedDisplayString = "Eggs,Toast,Coffee";

            Assert.IsTrue(addEntreeResult.IsSuccessful);
            Assert.IsTrue(addSideResult.IsSuccessful);
            Assert.IsTrue(addDrinkResult.IsSuccessful);
            Assert.AreEqual(expectedDisplayString, ordeModel.GetOrderString());
        }

        [TestMethod]
        public void Test_GetOrderString_At_Morning_Handles_Multiple_Drinks()
        {
            // Arrange
            var ordeModel = GetOrderModel(TimeOfDay.Morning);

            // Act
            var addSideResult = ordeModel.AddOrderItem(DishType.Side);
            var addEntreeResult = ordeModel.AddOrderItem(DishType.Entree);
            var addDrinkResult = ordeModel.AddOrderItem(DishType.Drink);
            var addSecondDrinkResult = ordeModel.AddOrderItem(DishType.Drink);

            // Assert

            //TODO: this could be more robust by using the actual MenuItems rather than hard coding the string for expeced value
            const string expectedDisplayString = "Eggs,Toast,Coffee(x2)";

            Assert.IsTrue(addEntreeResult.IsSuccessful);
            Assert.IsTrue(addSideResult.IsSuccessful);
            Assert.IsTrue(addDrinkResult.IsSuccessful);
            Assert.AreEqual(expectedDisplayString, ordeModel.GetOrderString());
        }

        #endregion

        #region Test: AddOrderItem

        [TestMethod]
        public void Test_AddOrderItem_At_Night_Allows_Max_One_Entree()
        {
            // Arrange
            var ordeModel = GetOrderModel(TimeOfDay.Night);

            // Act & Assert
            TestAddingMultipleEntrees(ordeModel);
        }        
        
        [TestMethod]
        public void Test_AddOrderItem_At_Morning_Allows_Max_One_Entree()
        {
            // Arrange
            var ordeModel = GetOrderModel(TimeOfDay.Morning);

            // Act & Assert
            TestAddingMultipleEntrees(ordeModel);
        }

        [TestMethod]
        public void Test_AddOrderItem_At_Night_Allows_Multiple_Sides()
        {
            // Arrange
            var ordeModel = GetOrderModel(TimeOfDay.Night);

            // Act
            var firstAddSideResult = ordeModel.AddOrderItem(DishType.Side);
            var secondAddSideResult = ordeModel.AddOrderItem(DishType.Side);

            // Assert
            Assert.IsTrue(firstAddSideResult.IsSuccessful);
            Assert.IsTrue(secondAddSideResult.IsSuccessful);
        }

        [TestMethod]
        public void Test_AddOrderItem_At_Morning_Resitricts_Multiple_Sides()
        {
            // Arrange
            var ordeModel = GetOrderModel(TimeOfDay.Morning);

            // Act
            var firstAddSideResult = ordeModel.AddOrderItem(DishType.Side);
            var secondAddSideResult = ordeModel.AddOrderItem(DishType.Side);

            // Assert
            Assert.IsTrue(firstAddSideResult.IsSuccessful);
            Assert.IsTrue(secondAddSideResult.IsFailure);
        }

        [TestMethod]
        public void Test_AddOrderItem_At_Night_Allows_Max_One_Drink()
        {
            // Arrange
            var ordeModel = GetOrderModel(TimeOfDay.Night);

            // Act
            var firstAddDrinkResult = ordeModel.AddOrderItem(DishType.Drink);
            var secondAddDrinkResult = ordeModel.AddOrderItem(DishType.Drink);

            // Assert
            Assert.IsTrue(firstAddDrinkResult.IsSuccessful);
            Assert.IsTrue(secondAddDrinkResult.IsFailure);
        }        
        
        [TestMethod]
        public void Test_AddOrderItem_At_Morning_Allows_Multiple_Drinks()
        {
            // Arrange
            var ordeModel = GetOrderModel(TimeOfDay.Morning);

            // Act
            var firstAddDrinkResult = ordeModel.AddOrderItem(DishType.Drink);
            var secondAddDrinkResult = ordeModel.AddOrderItem(DishType.Drink);

            // Assert
            Assert.IsTrue(firstAddDrinkResult.IsSuccessful);
            Assert.IsTrue(secondAddDrinkResult.IsSuccessful);
        }

        [TestMethod]
        public void Test_AddOrderItem_At_Night_Allows_Max_One_Desert()
        {
            // Arrange
            var ordeModel = GetOrderModel(TimeOfDay.Night);

            // Act
            var firstAddDesertResult = ordeModel.AddOrderItem(DishType.Desert);
            var secondAddDesertResult = ordeModel.AddOrderItem(DishType.Desert);

            // Assert
            Assert.IsTrue(firstAddDesertResult.IsSuccessful);
            Assert.IsTrue(secondAddDesertResult.IsFailure);
        }             
        
        [TestMethod]
        public void Test_AddOrderItem_At_Morning_Has_No_Desert()
        {
            // Arrange
            var ordeModel = GetOrderModel(TimeOfDay.Morning);

            // Act
            var addDesertResult = ordeModel.AddOrderItem(DishType.Desert);

            // Assert
            Assert.IsTrue(addDesertResult.IsFailure);
        }

        #endregion

        #region Privates

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ordeModel"></param>
        private static void TestAddingMultipleEntrees(OrderModel ordeModel)
        {
            // Act
            var firstEntreeResult = ordeModel.AddOrderItem(DishType.Entree);
            var secondEntreeResult = ordeModel.AddOrderItem(DishType.Entree);

            // Assert
            Assert.IsTrue(firstEntreeResult.IsSuccessful);
            Assert.IsTrue(secondEntreeResult.IsFailure);
        }

        private OrderModel GetOrderModel(TimeOfDay timeOfDay)
        {
            var orderModel = new OrderModel(timeOfDay);

            return orderModel;
        }

        #endregion
    }
}
