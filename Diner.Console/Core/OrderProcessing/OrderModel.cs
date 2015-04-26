using System;
using System.Collections.Generic;
using System.Linq;
using Diner.Enums;
using Diner.MenuItems;

namespace Diner.Core.OrderProcessing
{
    /// <summary>
    /// Order domain model, handles all order specific rules (number of items, menu for TimeOfDay, etc)
    /// </summary>
    public class OrderModel
    {
        /// <summary>
        /// Time of day for current order
        /// </summary>
        private readonly TimeOfDay _timeOfDay;

        /// <summary>
        /// Currently added menu items to the order
        /// </summary>
        private readonly List<IMenuItem> _menuItems;

        public OrderModel(TimeOfDay timeOfDay)
        {
            _timeOfDay = timeOfDay;
            _menuItems = new List<IMenuItem>();
        }

        public OperationResult<IMenuItem> AddOrderItem(DishType dishType)
        {
            switch (dishType)
            {
                case DishType.Entree:
                    return AddEntree();
                case DishType.Side:
                    return AddSide();
                case DishType.Desert:
                    return AddDesert();
                case DishType.Drink:
                    return AddDrink();
                default:
                    throw new NotSupportedException("Dish type not supported");
            }
        }

        public string GetOrderString()
        {
            var orderString = new List<string>();
        
            AppendDishString(DishType.Entree, orderString);
            AppendDishString(DishType.Side, orderString);
            AppendDishString(DishType.Drink, orderString);
            AppendDishString(DishType.Desert, orderString);

            var formattedString = string.Join(",", orderString);

            return formattedString;
        }

        #region Privates

        /// <summary>
        /// Append display value of menu item to list order string
        /// </summary>
        /// <param name="dishType"></param>
        /// <param name="orderString"></param>
        private void AppendDishString(DishType dishType, List<string> orderString)
        {
            var items = _menuItems.Where(x => x.DishType == dishType);

            if (!items.Any())
            {
                return;
            }

            var itemCount = items.Count();
            var itemName = items.First().Name;

            var menuItemString = (itemCount == 1)
                                     ? itemName
                                     : string.Format("{0}(x{1})", itemName, itemCount);

            orderString.Add(menuItemString);
        }

        private OperationResult<IMenuItem> AddEntree()
        {
            var result = new OperationResult<IMenuItem>();
            var canAddEntree = CanAddEntree();

            if (!canAddEntree)
            {
                result.SetAsFail("Cannot add entree");
                return result;
            }

            var entreeItem = _timeOfDay == TimeOfDay.Morning
                                 ? (IMenuItem) new MenuItemEggs()
                                 : (IMenuItem) new MenuItemSteak();

            _menuItems.Add(entreeItem);
            result.SetAsSuccess(entreeItem);

            return result;
        }

        private OperationResult<IMenuItem> AddSide()
        {
            var result = new OperationResult<IMenuItem>();
            var canAddSide = CanAddSide();

            if (!canAddSide)
            {
                result.SetAsFail("Cannot add side");
                return result;
            }

            var sideItem = _timeOfDay == TimeOfDay.Morning
                               ? (IMenuItem) new MenuItemToast()
                               : (IMenuItem) new MenuItemPotato();

            _menuItems.Add(sideItem);
            result.SetAsSuccess(sideItem);

            return result;
        }

        private OperationResult<IMenuItem> AddDesert()
        {
            var result = new OperationResult<IMenuItem>();
            var canAddSide = CanAddDesert();

            if (!canAddSide)
            {
                result.SetAsFail("Cannot add desert");
                return result;
            }

            var desertItem = new MenuItemCake();

            _menuItems.Add(desertItem);
            result.SetAsSuccess(desertItem);

            return result;
        }

        private OperationResult<IMenuItem> AddDrink()
        {
            var result = new OperationResult<IMenuItem>();
            var canAddSide = CanAddDrink();

            if (!canAddSide)
            {
                result.SetAsFail("Cannot add drink");
                return result;
            }

            var drinkItem = _timeOfDay == TimeOfDay.Morning
                                ? (IMenuItem) new MenuItemCoffee()
                                : (IMenuItem) new MenuItemWine();

            _menuItems.Add(drinkItem);
            result.SetAsSuccess(drinkItem);

            return result;
        }

        private bool CanAddEntree()
        {
            bool canAddEntree = _menuItems.Count(x => x.DishType == DishType.Entree) == 0;

            return canAddEntree;
        }

        private bool CanAddSide()
        {
            if (_timeOfDay == TimeOfDay.Night)
            {
                return true;
            }

            int countOfSides = _menuItems.Count(x => x.DishType == DishType.Side);

            return countOfSides == 0;
        }

        private bool CanAddDesert()
        {
            if (_timeOfDay == TimeOfDay.Night)
            {
                int countOfDeserts = _menuItems.Count(x => x.DishType == DishType.Desert);

                return countOfDeserts == 0;
            }

            return false;
        }

        private bool CanAddDrink()
        {
            if (_timeOfDay == TimeOfDay.Night)
            {
                int countOfDrinks = _menuItems.Count(x => x.DishType == DishType.Drink);

                return countOfDrinks == 0;
            }

            return true;
        }

        #endregion
    }
}